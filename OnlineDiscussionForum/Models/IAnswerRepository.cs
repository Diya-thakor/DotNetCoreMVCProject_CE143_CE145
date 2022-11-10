using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDiscussionForum.Models
{
    public interface IAnswerRepository
    {
        Answer GetAnswer(int id);
        IEnumerable<Answer> GetAnswerByQuestion(int questionId);
        IEnumerable<Answer> getAnswerByUser(int userId);

        IEnumerable<Answer> GetAllAnswer();

        Answer Add(Answer answer);
        Answer update(Answer answerChanges);
        Answer delete(int id);
    }
}
