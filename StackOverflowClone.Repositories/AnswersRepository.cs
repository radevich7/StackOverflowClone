using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackOverflowClone.DomainModels;

namespace StackOverflowClone.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void UpdateAnswerVotesCount(int answerID, int userID, int value);
        void DeleteAnswer(int answerID);
        List<Answer> GetAnswersByQuestionID(int questionID);
        List<Answer> GetAnswersByAnswerID(int answerID);
    }

    public class AnswersRepository : IAnswersRepository
    {
        StackOverflowDbContext db;
        IQuestionsRepository questionRepository;
        IVotesRepository votesRepository;

        public AnswersRepository()
        {
            db = new StackOverflowDbContext();
            questionRepository = new QuestionsRepository();
            votesRepository = new VotesRepository();

        }

        public void DeleteAnswer(int answerID)
        {
            Answer existingAnswer = db.Answers.Where(temp => temp.AnswerID == answerID).FirstOrDefault();

            if (existingAnswer != null)
            {
                db.Answers.Remove(existingAnswer);
                db.SaveChanges();
                questionRepository.UpdateQuestionAnswerCount(existingAnswer.AnswerID, -1);
            }
        }

        public List<Answer> GetAnswersByAnswerID(int answerID)
        {
            List<Answer> answersByID = db.Answers.Where((temp) => temp.AnswerID == answerID).OrderByDescending(temp => temp.AnswerDateAndTime).ToList();
            return answersByID;
        }

        public List<Answer> GetAnswersByQuestionID(int questionID)
        {
            List<Answer> answersByQuestionID = db.Answers.Where(temp => temp.QuestionID == questionID).OrderByDescending(temp => temp.AnswerDateAndTime).ToList();
            return answersByQuestionID;
        }

        public void InsertAnswer(Answer answer)
        {
            db.Answers.Add(answer);
            db.SaveChanges();
            questionRepository.UpdateQuestionAnswerCount(answer.QuestionID, 1);
        }

        public void UpdateAnswer(Answer answer)
        {
            Answer existingAnswer = db.Answers.Where(temp => temp.AnswerID == answer.AnswerID).FirstOrDefault();

            if (existingAnswer != null)
            {
                existingAnswer.AnswerDateAndTime = answer.AnswerDateAndTime;
                existingAnswer.AnswerText = answer.AnswerText;
                db.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int answerID, int userID, int value)
        {
            Answer existingAnswer = db.Answers.Where(temp => temp.AnswerID == answerID && temp.UserID == userID).FirstOrDefault();

            if (existingAnswer != null)
            {
                    existingAnswer.VotesCount += value;
                    db.SaveChanges();
                    questionRepository.UpdateQuestionVotesCount(answerID, value);
                    votesRepository.UpdateVotesCount(answerID, userID, value);
                
            }

        }
    }
}
