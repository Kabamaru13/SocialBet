using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialBet.Models;

namespace SocialBet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private async Task<IEnumerable<User>> GetUsers()
        {
            var users = new User[]{
                new User { Id = 1, Username = "Kabamaru13", FullName = "Sotiris Giagkiozis", Email = "test@gmail.com"},
                new User { Id = 2, Username = "Rival1", FullName = "Aggelos Papamichail", Email = "test@gmail.com"},
                new User { Id = 3, Username = "ReferreeGuy", FullName = "Test User", Email = "test@gmail.com"},
            };

            return users;
        }

        // GET api/user
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await GetUsers();
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            var users = await GetUsers();
            return users.FirstOrDefault(x => x.Id == id);
        }

        // POST api/
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api//5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api//5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
