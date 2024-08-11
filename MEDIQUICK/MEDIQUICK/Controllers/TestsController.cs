﻿using MEDIQUICK.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MEDIQUICK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        // GET: api/<TestsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("CalculateAndUpdateScoreAndGetDuration/{testId}")]
        public Object CalculateAndUpdateScoreAndGetDuration(int testId)
        {
            return new Test().CalculateAndUpdateScoreAndGetDuration(testId);
        }


        [HttpGet("GetTestSummary/{testId}")]
        public Object GetTestSummary(int testId)
        {
            return new Test().GetTestSummary(testId);
        }

        [HttpGet("GetQuestionDetailsInTest/{testId}")]
        public Object GetQuestionDetailsInTest(int testId)
        {
            return new Test().GetQuestionDetailsInTest(testId);
        }

        // POST api/<TestsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // POST api/<TestsController>
        [HttpPost("userId/{userId}")]
        public Object CreateTest(int userId)
        {
            Test t = new Test();
            return t.CreateTest( userId);

        }

        //// POST api/<TestsController>
        //[HttpPost("userId/{userId}/testId/{testId}/questionId/{questionId}/isCorrect/{isCorrect}")]

        //public Question Test_HandleQuestionAnswer(int userId, int testId, int questionId,bool isCorrect)
        //{
        //    Test t = new Test();
        //    return t.Test_HandleQuestionAnswer(userId,testId,questionId,isCorrect);
        //}

        // POST api/<TestsController>
        [HttpPost("/HandleTestQuestionAnswer")]
        public Question Test_HandleQuestionAnswer(TestRequestData trd)
        {
            Test t = new Test();
            return t.Test_HandleQuestionAnswer(trd.userId, trd.testId, trd.questionId, trd.isCorrect, trd.lastQ);
        }

        // POST api/<TestsController>
        [HttpPost("testId/{testId}")]
        public int EndTest(int testId)
        {
            Test t = new Test();
            return t.EndTest(testId);

        }

        // PUT api/<TestsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
