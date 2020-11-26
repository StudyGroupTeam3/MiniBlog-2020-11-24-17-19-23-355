using System.Collections.Generic;
using System.Linq;
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

        public List<User> GetAllUsers()
        {
            return userStore.Users;
        }

        public void Register(User user)
        {
            if (!userStore.Users.Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                userStore.Users.Add(user);
            }
        }

        public User FoundUserByName(string name)
        {
            return userStore.Users.FirstOrDefault(_ => _.Name == name);
        }

        public User DeleteUser(string name)
        {
            var foundUser = FoundUserByName(name);
            if (foundUser != null)
            {
                userStore.Users.Remove(foundUser);
                articleStore.Articles.RemoveAll(article => article.UserName == foundUser.Name);
            }

            return foundUser;
        }

        public User UpdateUser(User user)
        {
            var foundUser = FoundUserByName(user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }
    }
}
