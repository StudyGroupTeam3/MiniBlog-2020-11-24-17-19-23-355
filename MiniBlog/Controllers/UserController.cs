using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly IUser validUser;
        public UserController(IUser user, UserService userService)
        {
            this.userService = userService;
            validUser = user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        {
            this.userService.Register(user);

            return CreatedAtAction(nameof(GetByName), new { name = user.Name }, user);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return validUser.Users;
        }

        [HttpPut]
        public User Update(User user)
        {
            var foundUser = validUser.Users.FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        [HttpDelete]
        public User Delete(string name)
        {
            var foundUser = validUser.Users.FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                validUser.Users.Remove(foundUser);
                ArticleStoreWillReplaceInFuture.Articles.RemoveAll(a => a.UserName == foundUser.Name);
            }

            return foundUser;
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return validUser.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
        }
    }
}