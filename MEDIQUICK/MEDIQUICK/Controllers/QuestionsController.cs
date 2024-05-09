using MEDIQUICK.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MEDIQUICK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        // GET: api/<QuestionsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<QuestionsController>/5
        [HttpGet("{id}")]
        public Question Get(int id)
        {
            Question q = new Question();
            return q.GetQuestion(id);
        }

        // POST api/<QuestionsController>
        [HttpPost]
        public int InsertQuestion([FromBody] Question value)
        {
            return value.Insert();
        }

        // PUT api/<QuestionsController>/5
        [HttpPut]
        public void Put(Question q, bool isCorrect)
        {
            q.updateScore(q, isCorrect);
        }

        // DELETE api/<QuestionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
