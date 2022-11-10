using System.Collections.Generic;

namespace OnlineDiscussionForum.Models
{
    public class CollectionDataModel
    {
        public IEnumerable<QuestionHistory> Questions { get; set; }
        public IEnumerable<Answer> answers { get; set; }
        public readonly IUserRepository _userRepo;

        public CollectionDataModel(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
    }
}
