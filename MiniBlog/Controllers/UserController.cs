using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserStore iUserStore;

        public UserController(IUserStore iUserStore)
        {
            this.iUserStore = iUserStore;
        }

        [HttpPost]
        public async Task<ActionResult<Action>> Register(User user)
        {
            if (!iUserStore.Users.Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                iUserStore.Users.Add(user);
            }

            return CreatedAtAction(nameof(GetByName), new { Name = user.Name }, user);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return UserStoreWillReplaceInFuture.Users;
        }

        [HttpPut]
        public User Update(User user)
        {
            var foundUser = UserStoreWillReplaceInFuture.Users.FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        [HttpDelete]
        public User Delete(string name)
        {
            var foundUser = UserStoreWillReplaceInFuture.Users.FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                UserStoreWillReplaceInFuture.Users.Remove(foundUser);
                ArticleStoreWillReplaceInFuture.Articles.RemoveAll(a => a.UserName == foundUser.Name);
            }

            return foundUser;
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return UserStoreWillReplaceInFuture.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
        }
    }
}