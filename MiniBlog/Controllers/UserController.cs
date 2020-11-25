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

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        {
            userService.Regsiter(user);

            return CreatedAtAction(nameof(GetByName), new { Name = user.Name }, user);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return userService.GetAllUsers();
        }

        [HttpPut]
        public User Update(User user)
        {
            var foundUser = userService.GetUserByName(user.Name);
            return userService.PatchUser(foundUser, user);
        }

        [HttpDelete]
        public User Delete(string name)
        {
            var foundUser = userService.GetUserByName(name);
            return userService.DeleteUser(foundUser);
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return userService.GetUserByName(name);
        }
    }
}