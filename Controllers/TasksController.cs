using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>();
        private static int nextId = 1;

        // Создание задачи
        [HttpPost]
        public ActionResult<TaskItem> CreateTask([FromBody] TaskItem taskItem)
        {
            taskItem.Id = nextId++;
            tasks.Add(taskItem);
            return CreatedAtAction(nameof(GetTask), new { id = taskItem.Id }, taskItem);
        }

        // Получение списка задач с фильтрацией по статусу
        [HttpGet]
        public ActionResult<List<TaskItem>> GetTasks([FromQuery] string status = null)
        {
            var result = string.IsNullOrEmpty(status) ? tasks : tasks.Where(t => t.Status.ToLower() == status.ToLower()).ToList();
            return Ok(result);
        }

        // Получение задачи по ID
        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // Обновление существующей задачи
        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, [FromBody] TaskItem taskItem)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
            {
                return NotFound();
            }
            existingTask.Title = taskItem.Title;
            existingTask.Description = taskItem.Description;
            existingTask.Status = taskItem.Status;
            return NoContent();
        }

        // Удаление задачи
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
            {
                return NotFound();
            }
            tasks.Remove(existingTask);
            return NoContent();
        }
    }
}