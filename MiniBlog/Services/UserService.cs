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

        public UserService(IUserStore userStore)
        {
            this.userStore = userStore;
        }

        public void Register(User user)
        {
            if (!userStore.Users.Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                userStore.Users.Add(user);
            }
        }
    }
}
