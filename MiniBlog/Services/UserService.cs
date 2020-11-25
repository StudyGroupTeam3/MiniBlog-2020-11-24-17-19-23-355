using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public interface IUserService
    {
        void Register(User user);
    }

    public class UserService : IUserService
    {
        private IUserStore userStore;
        public UserService(IUserStore userStore)
        {
            this.userStore = userStore;
        }

        public void Register(User user)
        {
            if (!userStore.Users.Exists(x => user.Name.ToLower() == x.Name.ToLower()))
            {
                userStore.Users.Add(user);
            }
        }
    }
}
