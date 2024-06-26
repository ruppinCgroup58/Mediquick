﻿using MEDIQUICK.BL;
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
        [HttpGet("topicName/{topicName}/userId/{userId}")]
        public List<Object> GetQuestionsByTopic(string topicName,int userId)
        {
            Question q = new Question();
            return q.GetQuestionsByTopic(topicName,userId);
        }

        // GET: api/<QuestionsController>
        [HttpGet("userId/{userId}")]
        public List<Question> GetFavouriteQuestionsByUser(int userId)
        {
            Question q = new Question();
            return q.GetFavouriteQuestionsByUser(userId);
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

        // POST api/<QuestionsController>
        [HttpPost("questionId/{questionId}/userId/{userId}")]
        public int toggleFavouriteQuestion(int questionId, int userId)
        {
            Question q = new Question();
            return q.toggleFavouriteQuestion(questionId,userId);
                         
        }

        // PUT api/<QuestionsController>/5
        [HttpPut("{isCorrect}")]
        public void Put(bool isCorrect,[FromBody] int id)
        {
            Question q = new Question();
            q.updateQuestionDiffLevel(id, isCorrect);
        }

        [HttpPut("id/{id}")]
        public int Put(int id, [FromBody] int newStatus)
        {
            Question q = new Question();
           return q.changeQuestionStatus(id, newStatus);
        }

        [HttpPatch("/updateQuestionDetails")]
        public bool updateQuestionDetail([FromBody] Question q)
        {
            return new Question().updateQuestionDetail(q);
        }

        // DELETE api/<QuestionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
