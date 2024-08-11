using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly DBServices _dbServices = new DBServices();

    // GET: api/Tasks/GetTaskByDate
    [HttpGet("GetTaskByDate")]
    public ActionResult<TaskModel> GetTaskByDate(string userId, System.DateTime date)
    {
        TaskModel task = _dbServices.GetTaskByDate(userId, date);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    // POST: api/Tasks/SaveTask
    [HttpPost("SaveTask")]
    public ActionResult SaveTask([FromBody] TaskModel task)
    {
        int result = _dbServices.SaveTask(task);

        if (result > 0)
        {
            return Ok();
        }

        return BadRequest("Failed to save task.");
    }

    // DELETE: api/Tasks/DeleteTask/{id}
    [HttpDelete("DeleteTask/{id}")]
    public ActionResult DeleteTaskCalender(int id)
    {
        int result = _dbServices.DeleteTask(id);

        if (result > 0)
        {
            return Ok();
        }

        return NotFound("Task not found.");
    }
}
