using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class UserService
    {
        private IUserStore userStore;
        private IArticleStore articleStore;

        public UserService(IUserStore userStore, IArticleStore articleStore)
        {
            this.userStore = userStore;
            this.articleStore = articleStore;
        }

        public User FindUserByName(string name)
        {
            return userStore.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
        }

        public void Register(User user)
        {
            if (FindUserByName(user.Name) is null)
            {
                userStore.Users.Add(user);
            }
        }

        public User UpdateUser(User user)
        {
            var foundUser = FindUserByName(user.Name);
            if (!(foundUser is null))
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        public User DeleteUser(string name)
        {
            var foundUser = FindUserByName(name);
            if (!(foundUser is null))
            {
                userStore.Users.Remove(foundUser);
                articleStore.Articles.RemoveAll(a => a.UserName == foundUser.Name);
            }

            return foundUser;
        }
    }
}
