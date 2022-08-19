using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackOverflowClone.DomainModels;

namespace StackOverflowClone.Repositories
{

    public interface IVotesRepository
    {
        void UpdateVotesCount(int answerID, int userID, int value);
    }
    public class VotesRepository : IVotesRepository
    {
        StackOverflowDbContext db;
        QuestionsRepository questionRepository;
        AnswersRepository answerRepository;
        public VotesRepository()
        {
            db = new StackOverflowDbContext();
            questionRepository = new QuestionsRepository();
            answerRepository = new AnswersRepository();
        }
        public void UpdateVotesCount(int answerID, int userID, int value)
        {
            int updatedValue;
            if (value > 0) { updatedValue = 1; }
            else if (value < 0) { updatedValue = -1; }
            else { updatedValue = 0; }
            Vote existingVote = db.Votes.Where(temp => temp.AnswerID == answerID && temp.UserID == userID).FirstOrDefault();

            if (existingVote != null)
            {
                existingVote.VoteValue += updatedValue;
            }
            else
            {
                Vote newVote = new Vote() { AnswerID = answerID, UserID = userID, VoteValue = updatedValue };
                db.Votes.Add(newVote);
            }
            db.SaveChanges();
            questionRepository.UpdateQuestionVotesCount(userID, updatedValue);
            answerRepository.UpdateAnswerVotesCount(answerID, userID, updatedValue);

        }
    }
}
