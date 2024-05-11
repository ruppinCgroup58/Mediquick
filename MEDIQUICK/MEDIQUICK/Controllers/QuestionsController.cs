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
        public Task<string> Get()
        {
            Question question = new Question(); 
            return question.genQ();
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
            q.updateQuestionDiffLevel(q, isCorrect);
        }

        // DELETE api/<QuestionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
