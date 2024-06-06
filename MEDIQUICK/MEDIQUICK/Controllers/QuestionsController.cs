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
        [HttpPost("/Gemini")]
        public Task<string> Post([FromBody] string content)
        {
            Question question = new Question(); 
            return question.genQ(content);
        }

        // GET: api/<QuestionsController>
        [HttpGet("/ReadQuestions")]
        public IEnumerable<Question> ReadQuestions()
        {
            Question q = new Question();
            return q.ReadQuestions();
        }

        // GET: api/<QuestionsController>
        [HttpGet("topicName/{topicName}")]
        public List<Question> GetQuestionsByTopic(string topicName)
        {
            Question q = new Question();
            return q.GetQuestionsByTopic(topicName);
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
        [HttpPut("{isCorrect}")]
        public void Put(bool isCorrect,[FromBody] int id)
        {
            Question q = new Question();
            q.updateQuestionDiffLevel(id, isCorrect);
        }

        [HttpPut("id/{id}")]
        public void Put(int id, [FromBody] int newStatus)
        {
            Question q = new Question();
            q.changeQuestionStatus(id, newStatus);
        }


        // DELETE api/<QuestionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
