using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDiscussionForum.Models
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        public SQLUserRepository(AppDbContext context)
        {
            this.context = context;
        }

        User IUserRepository.Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        User IUserRepository.Delete(int Id)
        {
            User user = context.Users.Find(Id);
            if(user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return user;
        }

        IEnumerable<User> IUserRepository.GetAllUsers()
        {
            return context.Users;
        }

        User IUserRepository.GetUser(int Id)
        {
            return context.Users.FirstOrDefault(user => user.Id == Id);
        }

        User IUserRepository.GetUser(string username)
        {
            return context.Users.FirstOrDefault(user => user.Name == username);
        }

        User IUserRepository.GetUser(string userName, string password)
        {
            return context.Users.FirstOrDefault(user => user.Name == userName && user.password == password);
        }


        User IUserRepository.Update(User userChanges)
        {
            var user = context.Users.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return userChanges;
        }
    }
}
