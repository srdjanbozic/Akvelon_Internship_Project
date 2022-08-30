using Internship_Task.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Task = Internship_Task.Models.Task;

namespace Internship_Task.Repositories
{
    public interface IRepository<T> where T : class
    {
        // Repository Pattern provides an abstraction over the data layer
        // It will be implemented by base entity class
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);  
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IEnumerable<Task> FindTasksForProject(Project project);
        Project FindProjectForTask(Task task);

    }
}
