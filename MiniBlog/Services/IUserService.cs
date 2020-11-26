using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;

namespace MiniBlog.Services
{
    public interface IUserService
    {
        void Register(User user);
        User FoundUserWhileUpdate(User user);
        User FoundUserWhileDelete(string name);
        User FoundUserByName(string name);
        List<User> GetAllUsers();
    }
}
