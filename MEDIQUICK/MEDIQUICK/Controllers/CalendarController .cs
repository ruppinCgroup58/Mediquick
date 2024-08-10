using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MEDIQUICK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private DBServices dbServices = new DBServices();

        [HttpPost]
        public IActionResult SaveTask([FromBody] TaskModel taskModel)
        {
            dbServices.SaveTask(taskModel.UserId, taskModel.Date, taskModel.Task);
            return Ok(new { message = "Task saved successfully" });
        }

        [HttpGet("{userId}/{date}")]
        public IActionResult GetTask(string userId, string date)
        {
            string task = dbServices.GetTask(userId, date);
            return Ok(new { task });
        }

        [HttpDelete("{userId}/{date}")]
        public IActionResult DeleteTask(string userId, string date)
        {
            dbServices.DeleteTask(userId, date);
            return Ok(new { message = "Task deleted successfully" });
        }
    }

    public class TaskModel
    {
        public string UserId { get; set; }
        public string Date { get; set; }
        public string Task { get; set; }
    }

}
