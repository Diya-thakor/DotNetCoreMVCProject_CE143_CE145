using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDiscussionForum.Models
{
    public interface IUserRepository
    {
        User GetUser(int Id);
        User GetUser(string username);
        User GetUser(String userName, String password);
        IEnumerable<User> GetAllUsers();
        User Add(User user);
        User Update(User userChanges);
        User Delete(int Id);
    }
}
