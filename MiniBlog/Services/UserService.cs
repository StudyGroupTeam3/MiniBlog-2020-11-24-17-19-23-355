using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class UserService : IUserService
    {
        private IUserStore userStore;
        private IArticleStore articleStore;
        public UserService(IUserStore userStore, IArticleStore articleStore)
        {
            this.userStore = userStore;
            this.articleStore = articleStore;
        }

        public void Register(User user)
        {
            if (!userStore.Users.Exists(x => user.Name.ToLower() == x.Name.ToLower()))
            {
                userStore.Users.Add(user);
            }
        }

        public User FoundUserWhileUpdate(User user)
        {
            var foundUser = FoundUserByNameWithCaseSensitive(user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        public User FoundUserWhileDelete(string name)
        {
            var foundUser = FoundUserByNameWithCaseSensitive(name);
            if (foundUser != null)
            {
                userStore.Users.Remove(foundUser);
                articleStore.Articles.RemoveAll(a => a.UserName == foundUser.Name);
            }

            return foundUser;
        }

        public User FoundUserByName(string name)
        {
            return userStore.Users.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        public User FoundUserByNameWithCaseSensitive(string name)
        {
            return userStore.Users.FirstOrDefault(x => x.Name == name);
        }

        public List<User> GetAllUsers()
        {
            return userStore.Users;
        }
    }
}
