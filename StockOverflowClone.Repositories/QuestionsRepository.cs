using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackOverflowClone.DomainModels;
namespace StockOverflowClone.Repositories
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question question);
        void DeleteQuestion(int questionID);
        void UpdateQuestionDetails(Question question);
        void UpdateQuestionVotesCount(int questionID, int value);
        void UpdateQuestionAnswerCount(int questionID, int value);
        void UpdateQuestionViewCount(int questionID, int value);
        List<Question> GetAllQuestions();
        List<Question> GetQuestionByQuestionID(int questionID);



    }
    public class QuestionsRepository : IQuestionsRepository
    {
        StackOverflowDbContext db;

        public QuestionsRepository()
        {
            db = new StackOverflowDbContext();
        }

        public void DeleteQuestion(int questionID)
        {
            Question existingQuestion = db.Questions.Where(temp => temp.QuestionID == questionID).FirstOrDefault();
            if (existingQuestion != null)
            {
                db.Questions.Remove(existingQuestion);
                db.SaveChanges();
            }

        }

        public List<Question> GetAllQuestions()
        {
            List<Question> questions = db.Questions.OrderByDescending(temp => temp.QuestionDateAndTime).ToList();
            return questions;
        }

        public List<Question> GetQuestionByQuestionID(int questionID)
        {
            List<Question> questionsByID = db.Questions.Where(temp => temp.QuestionID == questionID).ToList();
            return questionsByID;
        }

        public void InsertQuestion(Question question)
        {
            db.Questions.Add(question);
            db.SaveChanges();
        }

        public void UpdateQuestionAnswerCount(int questionID, int value)
        {
            Question existingQuestion = db.Questions.Where(temp => temp.QuestionID == questionID).FirstOrDefault();

            if (existingQuestion != null)
            {
                existingQuestion.AnswersCount += value;
                db.SaveChanges();
            }


        }

        public void UpdateQuestionDetails(Question question)
        {
            Question existingQuestion = db.Questions.Where(temp => temp.QuestionID == question.QuestionID).FirstOrDefault();

            if (existingQuestion != null)
            {
                existingQuestion.QuestionName = question.QuestionName;
                existingQuestion.QuestionDateAndTime = question.QuestionDateAndTime;
                existingQuestion.CategoryID = question.CategoryID;
                db.SaveChanges();

            }
        }

        public void UpdateQuestionViewCount(int questionID, int value)
        {
            Question existingQuestion = db.Questions.Where(temp => temp.QuestionID == questionID).FirstOrDefault();

            if (existingQuestion != null)
            {

                existingQuestion.ViewsCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int questionID, int value)
        {
            Question existingQuestion = db.Questions.Where(temp => temp.QuestionID == questionID).FirstOrDefault();

            if (existingQuestion != null)
            {
                existingQuestion.VotesCount += value;
                db.SaveChanges();
            }
        }
    }
}
