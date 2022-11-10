using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineDiscussionForum.Models
{
    public class QuestionHistory
    {

        public int Id { get; set; }

        [Required]
        public string text { get; set; }

        [Required]
        public int userId { get; set; }
        public User User { get; set; }

        public IList<Answer> Answers { get; set; }
    }
}
