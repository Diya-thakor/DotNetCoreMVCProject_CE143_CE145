using System;
using System.Collections;
using System.Collections.Generic;

namespace OnlineDiscussionForum.Models
{
    public interface IQuestionRepository
    {
        QuestionHistory GetQuestion(int Id);
        IEnumerable<QuestionHistory> GetQuestionsByUser(int Id);
        QuestionHistory Add(QuestionHistory question, int id);
        QuestionHistory Update(QuestionHistory question);
        QuestionHistory Delete(int Id);

        IEnumerable<QuestionHistory> GetAllQuestions();

    }
}
