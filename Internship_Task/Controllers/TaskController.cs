using Internship_Task.Data;
using Internship_Task.Models;
using Internship_Task.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using Task = Internship_Task.Models.Task;

namespace Internship_Task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TaskController : ControllerBase
    {
        private readonly IRepository<Task> _repo;
        public TaskController(IRepository<Task> repo)
        {
            this._repo = repo;
        }
        /// <summary>
        ///  Returns all tasks from the db
        /// </summary>
        /// <returns> All the tasks from the database </returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Returns all the tasks from the database")]
        [SwaggerResponse(200, "Successfully returning all tasks.")]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Something is wrong with the server.")]
        //GET: api/tasks
        public IEnumerable<Task> GetAllTasks()
        {
            IEnumerable<Task> _tasks = _repo.GetAll();
            foreach (Task t in _tasks)
            {
                _repo.FindProjectForTask(t);
            }
            return _tasks;
        }
        /// <summary>
        ///  Returns task with the passed Id
        /// </summary>
        /// <param name="id"> Task Id </param>
        /// <returns> Task with specified Id </returns>
        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Returns the task with specified Id from the database")]
        [SwaggerResponse(200, "Successfully returning specified task.")]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Something is wrong with the server.")]
        //GET: api/tasks/1
        public Task GetTaskById(int id)
        {
            Task _task = _repo.GetById(id);
            _repo.FindProjectForTask(_task);
            return _task;
        }
        /// <summary>
        /// Creates new task
        /// </summary>
        /// <param name="task"></param>
        /// <returns> Newly created task </returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Adds new task to the database")]
        [SwaggerResponse(200, "Successfully added task to the database.")]
        [SwaggerResponse(201,"Created at action.")]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Something is wrong with the server.")]
        //POST: api/tasks
        public IActionResult CreateTask([FromBody] Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _repo.Add(task);
            return CreatedAtAction("Get", new { id = task.Id }, task);
        }
        /// <summary>
        /// Updates task with passed Id
        /// </summary>
        /// <param name="id"> Task Id</param>
        /// <param name="task"> Task name</param>
        /// <returns> Updated task </returns>
        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Update the task with specifed Id in the database")]
        [SwaggerResponse(200, "Successfully updated task.")]
        [SwaggerResponse(201,"Created at action.")]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Something is wrong with the server.")]
        //PUT: api/tasks/1
        public IActionResult UpdateTask(int id, [FromBody] Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            task.Id = id;
            _repo.Update(task);
            return CreatedAtAction("Put", task);
        }
        /// <summary>
        /// Delete task from the database
        /// </summary>
        /// <param name="id"> Task name</param>
        /// <returns></returns
        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deletes specified task from the database")]
        [SwaggerResponse(200, "Successfully deleted task.")]
        [SwaggerResponse(204, "Succesfully deleted task,no additional content")]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Something is wrong with the server.")]
        //DELETE: api/tasks/1
        public IActionResult DeleteTask(int id)
        {
            //_repo.Delete(id); 
            var existingTask = _repo.GetById(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            _repo.Delete(id);
            return NoContent();

        }
    }
}
