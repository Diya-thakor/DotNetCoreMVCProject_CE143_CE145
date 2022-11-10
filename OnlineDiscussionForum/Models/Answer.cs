using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDiscussionForum.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string text { get; set; }

        public int noOfLike { get; set; }
        
        //Accepted by the person who had asked the question or not
        public string status { get; set; }

        public int QuestionId { get; set; }
        public QuestionHistory Question { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}
