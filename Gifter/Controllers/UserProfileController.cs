using Gifter.Models;
using Gifter.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        // GET: api/<UserProfileController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userProfileRepository.GetAll());
        }

        // GET api/<UserProfileController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var profile = _userProfileRepository.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpGet("GetWithPosts/{id}")]
        public IActionResult GetWithPosts(int id)
        {
            var profile = _userProfileRepository.GetByIdWithPosts(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        // POST api/<UserProfileController>
        [HttpPost]
        public IActionResult Post(UserProfile profile)
        {
            _userProfileRepository.Add(profile);
            return CreatedAtAction("Get", new { id = profile.Id }, profile);
        }

        // PUT api/<UserProfileController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.Update(profile);
            return NoContent();
        }

        // DELETE api/<UserProfileController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepository.Delete(id);
            return NoContent();
        }
    }
}
