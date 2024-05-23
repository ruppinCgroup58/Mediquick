using MEDIQUICK.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MEDIQUICK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new User().ReadUsers();
        }

        [HttpPost("email/{email}/newStatus/{newStatus}")]
        public bool ChangeStatus(string email, bool newStatus)
        {
            //email/{email}/newStatus/{newStatus}
            return new User().ChangeUsersStatus(email, newStatus);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        // ***Registration***
        [HttpPost]
        public User Post([FromBody] User user)
        {
            return user.InsertUser();
        }

        // POST api/<UsersController>
        // ***Log in***
        [HttpPost("{email}")]
        public User Login(string email, [FromBody] string password)
        {
            return new User().Login(email, password);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
