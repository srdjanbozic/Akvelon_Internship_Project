using Internship_Task.Data;
using Internship_Task.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Internship_Task.Repositories;
using Task = Internship_Task.Models.Task;
using System.Linq.Expressions;

namespace Internship_Task.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Fields
        // Get the context for our DB
        protected readonly AppDbContext appDbContext;
        #endregion
        // Public constructor
        public Repository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        // Implementation of CRUD methods from the IRepository interface
        // Return all entities from the database
        public IEnumerable<T> GetAll()
        {
            return appDbContext.Set<T>().ToList();
        }
        // Return entity by Id from database
        public T GetById(int id)
        {
            return appDbContext.Set<T>().Find(id);
        }
        // Add new entity to the database
        public void Add(T entity)
        {
            appDbContext.Set<T>().Add(entity);
            appDbContext.SaveChanges();
        }
        // Remove passed entity
        public void Delete(int id)
        {
            var deleted = appDbContext.Set<T>().Find(id);
            appDbContext.Set<T>().Remove(deleted);
            appDbContext.SaveChanges();
        }
        //Update passed entity
        public void Update(T entity)
        {
            if(entity == null)
           {
                throw new ArgumentNullException();
            }
             appDbContext.Set<T>().Update(entity);
             appDbContext.SaveChanges();
        }
        public IQueryable<T> FindAll() => appDbContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => appDbContext.Set<T>().Where(expression).AsNoTracking();
     
        // Finds a specific project that is related to the task by same Id
        Project IRepository<T>.FindProjectForTask(Task task)
        {
            Project _project = appDbContext.Projects.Single(p => p.Id == task.ProjectId);
            return _project;
        }
        // Finds all the tasks that has same Id as passed project
        IEnumerable<Task> IRepository<T>.FindTasksForProject(Project project)
        {
            IEnumerable<Task> tasks = appDbContext.Tasks.Where(t => t.ProjectId == project.Id).Distinct().ToList();
            return tasks;
        }
    }
}
