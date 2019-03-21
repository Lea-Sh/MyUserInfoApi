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
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
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
            var result = await _service.SaveAsync(id, user);

            if (result.Status == Status.ValidationError)
            {
                return BadRequest();
            }
            if (result.Status == Status.Failed)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var result = await _service.AddAsync(user);

            if (result.Status == Status.ValidationError)
            {
                return BadRequest();
            }
            if (result.Status == Status.Failed)
            {
                return NotFound();
            }
            return CreatedAtAction($"GetUser", new {id = user.UserId}, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.Status == Status.NotFound || result.Status == Status.Failed)
            {
                return NotFound();
            }
            return result.Entity;
        }

    }
}
