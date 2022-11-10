using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDiscussionForum.Models
{
    public class SQLAnswerRepository : IAnswerRepository
    {

        private readonly AppDbContext context;
        public SQLAnswerRepository(AppDbContext context)
        {
            this.context = context;
        }
        Answer IAnswerRepository.Add(Answer answer)
        {
            context.Answers.Add(answer);
            context.SaveChanges();
            return answer;
        }

        Answer IAnswerRepository.delete(int id)
        {
            Answer answer = context.Answers.Find(id);
            if(answer != null)
            {
                context.Answers.Remove(answer);
                context.SaveChanges();
            }
            return answer;
        }

        IEnumerable<Answer> IAnswerRepository.GetAllAnswer()
        {
            return context.Answers;
        }

        Answer IAnswerRepository.GetAnswer(int id)
        {
            return context.Answers.FirstOrDefault(answer => answer.Id == id);
        }

        IEnumerable<Answer> IAnswerRepository.GetAnswerByQuestion(int questionId)
        {
            IEnumerable<Answer> answerList = context.Answers.Where(Answer => Answer.QuestionId == questionId);
            return answerList;
        }

        IEnumerable<Answer> IAnswerRepository.getAnswerByUser(int userId)
        {
            return context.Answers.Where(Answer => Answer.UserId == userId);
        }

        Answer IAnswerRepository.update(Answer answerChanges)
        {
            var answer = context.Answers.Attach(answerChanges);
            answer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return answerChanges;
        }
    }
}
