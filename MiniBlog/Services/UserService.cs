using System.Linq;
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

        public void Register(User user)
        {
            if (!userStore.Users.Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                userStore.Users.Add(user);
            }
        }

        public User FountUser(string name)
        {
            return userStore.Users.FirstOrDefault(_ => _.Name == name || _.Name.ToLower() == name.ToLower());
        }

        public User FountUser(User user)
        {
            return userStore.Users.FirstOrDefault(_ => _.Name == user.Name);
        }
    }
}
