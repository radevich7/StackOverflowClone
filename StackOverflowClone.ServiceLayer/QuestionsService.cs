using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StackOverflowClone.DomainModels;
using StackOverflowClone.ViewModels;
using StackOverflowClone.Repositories;

using AutoMapper;
using AutoMapper.Configuration;


namespace StackOverflowClone.ServiceLayer
{
    public interface IQuestionsService
    {
        void InsertQuestion(NewQuestionModel questionViewModel);
        void UpdateQuestionDetails(EditQuestionViewModel questionViewModel);
        void UpdateQuestionVotesCount(int questionID, int value);
        void UpdateQuestionAnswerCount(int questionID, int value);
        void UpdateQuestionViewCount(int questionID, int value);
        void DeleteQuestion(int questionID);
        List<QuestionViewModel> GetAllQuestions();
        QuestionViewModel GetQuestionByQuestionID(int questionID, int UserID);
    }


    public class QuestionsService : IQuestionsService
    {
        IQuestionsRepository qr;

        public QuestionsService()
        {
            qr = new QuestionsRepository();

        }

        public void DeleteQuestion(int questionID)
        {
            qr.DeleteQuestion(questionID);
        }

        public List<QuestionViewModel> GetAllQuestions()
        {
            List<Question> questionRep = qr.GetAllQuestions();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Category, CategoriesViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> questions = mapper.Map<List<Question>, List<QuestionViewModel>>(questionRep);
            return questions;
        }

        public QuestionViewModel GetQuestionByQuestionID(int questionID, int UserID = 0)
        {
            Question questionRep = qr.GetQuestionByQuestionID(questionID).FirstOrDefault();
            QuestionViewModel questionSer = null;
            if (questionRep == null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.CreateMap<Category, CategoriesViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();

                questionSer = mapper.Map<Question, QuestionViewModel>(questionRep);


                foreach (var item in questionSer.Answers)
                {

                    item.CurrentUserVoteType = 0;
                    VoteViewModel vote = item.Votes.Where(temp => temp.UserID == UserID).FirstOrDefault();
                    if (vote != null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }

            return questionSer;


        }

        public void InsertQuestion(NewQuestionModel questionViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewQuestionModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<NewQuestionModel, Question>(questionViewModel);
            qr.InsertQuestion(question);
        }

        public void UpdateQuestionAnswerCount(int questionID, int value)
        {
            qr.UpdateQuestionAnswerCount(questionID, value);
        }

        public void UpdateQuestionDetails(EditQuestionViewModel questionViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<EditQuestionViewModel, Question>(questionViewModel);
            qr.UpdateQuestionDetails(question);
        }

        public void UpdateQuestionViewCount(int questionID, int value)
        {
            qr.UpdateQuestionViewCount(questionID, value);
        }

        public void UpdateQuestionVotesCount(int questionID, int value)
        {
            qr.UpdateQuestionVotesCount(questionID, value);

        }
    }
}
