using Internship_Task.Data;
using Internship_Task.Models;
using Internship_Task.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Internship_Task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProjectController : ControllerBase
    {
        private readonly IRepository<Project> _repo;
        public ProjectController(IRepository<Project> repo)
        {
            this._repo = repo;
        }
        /// <summary>
        /// </summary>
        /// <returns> Return all projects from the database </returns>
        /// 
        [HttpGet]
        [SwaggerOperation(Summary = "Returns all the projects from the database")]
        [SwaggerResponse(200, "Successfully returning all projects.")]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Something is wrong with the server.")]
        // GET:api/projects
        public IEnumerable<Project> GetAllProjects()
        {
            var _projectsWithTasks = _repo.GetAll();
            foreach (var project in _projectsWithTasks)
            {
                _repo.FindTasksForProject(project);
            }
            return _projectsWithTasks;
        }
        /// <summary>
        /// Returns project by passed Id
        /// </summary>
        /// <param name="id"> Project Id</param>
        /// <returns> Project with specified Id </returns>
        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Returns the project with specified Id from the database")]
        [SwaggerResponse(200, "Successfully returning specified project.")]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Something is wrong with the server.")]
        //GET: api/projects/1
        public Project GetProjectById(int id)
        {
            Project _project = _repo.GetById(id);
            _repo.FindTasksForProject(_project);
            return _project;
        }
        /// <summary>
        /// Creates new post in the database
        /// </summary>
        /// <param name="project"> Project name </param>
        /// <returns> A newly created Project </returns>
        ///
        [HttpPost]
        [SwaggerOperation(Summary = "Adds new project to the database")]
        [SwaggerResponse(200, "Successfully added project to the database.")]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Something is wrong with the server.")]
        //POST: api/values
        public IActionResult CreateProject([FromBody]Project project)
        {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }
                _repo.Add(project);
                return CreatedAtAction("Get", new { id = project.Id }, project);
        }
        /// <summary>
        /// Update project by passed Id
        /// </summary>
        /// <param name="id"> Project Id </param>
        /// <param name="project"> Project Name </param>
        /// <returns> Updated project </returns>
        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Update the project with specifed Id in the database")]
        [SwaggerResponse(200, "Successfully updated project.")]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Something is wrong with the server.")]
        //PUT: api/values/1
        public IActionResult UpdateProject(int id,[FromBody]Project project)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                 project.Id = id; 
                _repo.Update(project);
                return CreatedAtAction("Put", project);
        }
        /// <summary>
        /// Delete project from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deletes project by Id from the database")]
        [SwaggerResponse(200, "Successfully deleted project.")]
        [SwaggerResponse(204, "Succesfully deleted project, no additional content")]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Something is wrong with the server.")]
        //DELETE: api/projects/1
        public IActionResult DeleteProject(int id)
        {
            //_repo.Delete(id); 
            var existingProject = _repo.GetById(id);
            if(existingProject == null)
            {
                return NotFound();
            }
            _repo.Delete(id);
            return NoContent();

        }
    }
}
