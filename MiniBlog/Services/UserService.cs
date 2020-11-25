using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly IUserStore userStore;
        private readonly IArticleStore articleStore;
        public UserService(IUserStore userStore, IArticleStore articleStore)
        {
            this.userStore = userStore;
            this.articleStore = articleStore;
        }

        public void Regsiter(User user)
        {
            if (!userStore.Users.Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                userStore.Users.Add(user);
            }
        }

        public void Regsiter(string articleUserName)
        {
            if (!userStore.Users.Exists(_ => articleUserName.ToLower() == _.Name.ToLower()))
            {
                userStore.Users.Add(new User(articleUserName));
            }
        }

        public User GetUserByName(string name)
        {
            return userStore.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
        }

        public List<User> GetAllUsers()
        {
            return userStore.Users;
        }

        public User PatchUser(User foundUser, User user)
        {
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        public User DeleteUser(User foundUser)
        {
            if (foundUser != null)
            {
                userStore.Users.Remove(foundUser);
                articleStore.Articles.RemoveAll(a => a.UserName == foundUser.Name);
            }

            return foundUser;
        }
    }
}
