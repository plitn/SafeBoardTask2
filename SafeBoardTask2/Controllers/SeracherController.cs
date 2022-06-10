using Microsoft.AspNetCore.Mvc;

namespace SafeBoardTask2.Controllers;

[ApiController]
[Route("[controller]")]
public class SearcherController : Controller
{
    private static Dictionary<int, TaskClass> _tasksDict = new Dictionary<int, TaskClass>();

    /// <summary>
    /// Обработчик гет запроса для получения информации о задании
    /// </summary>
    /// <param name="id">id задания</param>
    /// <returns>Статус + информацию</returns>
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

    /// <summary>
    /// Обработчик пост запроса на создание залания на сканирования
    /// </summary>
    /// <param name="directory">путь сканироваемой директории</param>
    /// <returns>Статус</returns>
    [HttpPost("scan")]
    public IActionResult startTask([FromQuery] string directory)
    {
        TaskClass task = new TaskClass(directory);
        task.Start();
        _tasksDict.Add(task.GetTaskId(), task);
        return Ok($"Task started {task.GetTaskId()}");
    }
}