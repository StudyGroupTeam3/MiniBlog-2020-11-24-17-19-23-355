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
        public UserService(IUserStore userStore)
        {
            this.userStore = userStore;
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
    }
}
