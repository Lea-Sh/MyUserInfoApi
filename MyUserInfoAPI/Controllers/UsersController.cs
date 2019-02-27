using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyUserInfoAPI.Data;
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
        private readonly UserService _service;

        public UsersController(IRepo<User> repo)
        {
            _repo = repo;

        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _repo.GetAllAsync();
        }
        
        // GET: api/Users/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repo.GetOneAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/Petrov
        [Route("lastname/{lastName}")]
        public IEnumerable<User> GetUsersByLastName(string lastName)
        {
            return (from u in _repo.GetAll() where u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) select u).ToList();
            // return _service.GetByLastName(lastName);
        }

        // GET: api/Users/name/Ivan
        [Route("name/{firstName}")]
        public IEnumerable<User> GetUsersByFirstName(string firstName)
        {
            return (from u in _repo.GetAll() where u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) select u).ToList();
            // return _service.GetByFirstName(firstName);
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
                await _repo.SaveAsync(user);
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

            await _repo.AddAsync(user);

            return CreatedAtAction($"GetUser", new {id = user.UserId}, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _repo.GetOneAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _repo.DeleteAsync(user);

            return user;
        }

        private bool UserExists(int id)
        {
            return _repo.GetAll().Count(e => e.UserId == id) > 0;
        }
    }
}
