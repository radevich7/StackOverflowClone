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
    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerViewModel answer);
        void UpdateAnswer(EditAnswerViewModel answer);
        void UpdateAnswerVotesCount(int AnswerID, int UserID, int Value);
        void DeleteAnswer(int AnswerID);
        List<AnswerViewModel> GetAnswersByQuestionID(int QuestionID);
        AnswerViewModel GetAnswerByAnswerID(int AnswerID);


    }
    public class AnswersService : IAnswersService
    {
        IAnswersRepository ar;
        public AnswersService()
        {
            ar = new AnswersRepository();
        }

        public void DeleteAnswer(int AnswerID)
        {
            ar.DeleteAnswer(AnswerID);
        }

        public List<AnswerViewModel> GetAnswersByQuestionID(int QuestionID)
        {
            List<Answer> answersRep = ar.GetAnswersByQuestionID(QuestionID);
            var config = new MapperConfiguration(cfg =>
             {
                 cfg.CreateMap<Answer, AnswerViewModel>();
                 cfg.IgnoreUnmapped();
             });

            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> answerList = mapper.Map<List<Answer>, List<AnswerViewModel>>(answersRep);

            return answerList;
        }

        public AnswerViewModel GetAnswerByAnswerID(int AnswerID)
        {
            Answer answerRep = ar.GetAnswersByAnswerID(AnswerID).FirstOrDefault();

            AnswerViewModel answerSer = null;
            if (answerRep != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.IgnoreUnmapped(); 
                });
                IMapper mapper = config.CreateMapper();
                answerSer = mapper.Map<Answer, AnswerViewModel>(answerRep);
            }
            return answerSer;
        }

        public void InsertAnswer(NewAnswerViewModel answer)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewAnswerViewModel, Answer>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();

            Answer answerSer = mapper.Map<NewAnswerViewModel, Answer>(answer);

            ar.InsertAnswer(answerSer);

        }

        public void UpdateAnswer(EditAnswerViewModel answer)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditAnswerViewModel, Answer>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();

            Answer answerSer = mapper.Map<EditAnswerViewModel, Answer>(answer);

            ar.UpdateAnswer(answerSer);
        }

        public void UpdateAnswerVotesCount(int AnswerID, int UserID, int Value)
        {
            ar.UpdateAnswerVotesCount(AnswerID, UserID, Value);
        }
    }
}
