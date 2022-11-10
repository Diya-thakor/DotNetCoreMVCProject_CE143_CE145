using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OnlineDiscussionForum.Models
{
    public class SQLQuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext context;
        public SQLQuestionRepository(AppDbContext dbContext)
        {
            context = dbContext;
        }
        QuestionHistory IQuestionRepository.GetQuestion(int Id)
        {
            return context.Questions.FirstOrDefault(m => m.Id == Id);
        }
        IEnumerable<QuestionHistory> IQuestionRepository.GetAllQuestions()
        {
            return context.Questions;
        }
        QuestionHistory IQuestionRepository.Add(QuestionHistory question, int id)
        {
            question.User = context.Users.FirstOrDefault(u => u.Id == id);
            context.Questions.Add(question);
            context.SaveChanges();
            return question;
        }
        QuestionHistory IQuestionRepository.Update(QuestionHistory questionChanges)
        {
            var que = context.Questions.Attach(questionChanges);
            que.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return questionChanges;
        }
        QuestionHistory IQuestionRepository.Delete(int Id)
        {
            QuestionHistory question = context.Questions.Find(Id);
            if (question != null)
            {
                context.Questions.Remove(question);
                context.SaveChanges();
            }
            return question;
        }

        public IEnumerable<QuestionHistory> GetQuestionsByUser(int Id)
        {
            return context.Questions.Where(x => x.userId == Id);
        }
    }
}
