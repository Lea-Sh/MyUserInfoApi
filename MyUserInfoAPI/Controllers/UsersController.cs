using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyUserInfoAPI.Models;
using MyUserInfoAPI.Repos;
using MyUserInfoAPI.Services;


namespace MyUserInfoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepo<User> _repo;
        private readonly IService<User> _service;

        public UsersController(IRepo<User> repo, IService<User> service)
        {
            _repo = repo;
            _service = service;

    }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _service.GetAllAsync();
        }
        
        // GET: api/Users/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _service.GetOneAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/name/Ivan
        [Route("name/{firstName}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByFirstName(string firstName)
        {
            return await _service.GetByFirstNameAsync(firstName);
        }

        // GET: api/Users/lastname/Petrov
        [Route("lastname/{lastName}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByLastName(string lastName)
        {
            return await _service.GetByLastNameAsync(lastName);
        }

        // PUT: api/Users/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != user.UserId)
            {
                return BadRequest();
            }
            try
            {
                await _service.SaveAsync(user);
            }
         
                catch (Exception ex)
            {
                //Production app should do more here
                throw;
            }
            return NoContent();
           
        }
        
       // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.AddAsync(user);

            return CreatedAtAction($"GetUser", new {id = user.UserId}, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _service.GetOneAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(user);

            return user;
        }

//        private bool UserExists(int id)
//        {
//            return _repo.GetAll().Count(e => e.UserId == id) > 0;
//        }
    }
}
