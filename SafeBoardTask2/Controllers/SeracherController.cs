using Microsoft.AspNetCore.Mvc;

namespace SafeBoardTask2.Controllers;

[ApiController]
[Route("[controller]")]
public class SearcherController : Controller
{
    private static Dictionary<int, TaskClass> _tasksDict = new Dictionary<int, TaskClass>();

    [HttpGet("status")]
    public IActionResult getTaskInfo([FromQuery] int id)
    {
        if (!_tasksDict.ContainsKey(id))
        {
            return Ok("error");
        }
        if (!_tasksDict[id].IsTaskCompleted())
        {
            return Ok("task is not completed");
        }
        return Ok(_tasksDict[id].GetInfo());
    }

    [HttpPost("scan")]
    public IActionResult startTask([FromQuery] string directory)
    {
        TaskClass task = new TaskClass(directory);
        task.Start();
        _tasksDict.Add(task.GetTaskId(), task);
        return Ok($"Task started {task.GetTaskId()}");
    }
}