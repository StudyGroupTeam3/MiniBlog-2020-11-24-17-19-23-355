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
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            userService.Register(user);

            return CreatedAtAction(nameof(GetByName), new { name = user.Name }, user);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return UserStoreWillReplaceInFuture.Users;
        }

        [HttpPut]
        public User Update(User user)
        {
            return userService.FoundUserWhileUpdate(user);
        }

        [HttpDelete]
        public User Delete(string name)
        {
            return userService.FoundUserWhileDelete(name);
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return userService.FoundUserByName(name);
        }
    }
}