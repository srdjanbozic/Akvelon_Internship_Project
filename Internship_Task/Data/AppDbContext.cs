using Internship_Task.Models;
using Microsoft.EntityFrameworkCore;
using Task = Internship_Task.Models.Task;


namespace Internship_Task.Data
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }

        public AppDbContext()
        {
        }

        // Initial data seeding to the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Projects Table
            modelBuilder.Entity<Project>().HasData(
                new Project()
                {
                    Id = 1,
                    ProjectName = "Machine Learning",
                    StartDate = new DateTime(2022, 2, 12),
                    CompletionDate = new DateTime(2023, 2, 12),
                    Status = Models.Project.ProjectStatus.Active,
                    Priority = 3
                });
             modelBuilder.Entity<Project>().HasData(
                    new Project()
                    {
                        Id = 2,
                        ProjectName = "EShop",
                        StartDate = new DateTime(2021, 5, 10),
                        CompletionDate = new DateTime(2022, 2, 12),
                        Status = Models.Project.ProjectStatus.Completed,
                        Priority = 1
                    });
              modelBuilder.Entity<Project>().HasData(
                      new Project()
                      {
                          Id = 3,
                          ProjectName = "Front-End",
                          StartDate = new DateTime(2022, 10, 12),
                          CompletionDate = new DateTime(2024, 12, 12),
                          Status = Models.Project.ProjectStatus.NotStarted,
                          Priority = 2
                      });
             // Seed Tasks Table
              modelBuilder.Entity<Task>().HasData(
                    new Task()
                    {
                        Id = 1,
                        TaskName = "Sorting",
                        Status = Models.Task.TaskStatus.InProgress,
                        TaskPriority = 3,
                        TaskDescription = "Implement merge sorting algorithm.",
                        ProjectId = 1
                    });
               modelBuilder.Entity<Task>().HasData(
                  new Task()
                  {
                      Id = 2,
                      TaskName = "RestFul Services",
                      Status = Models.Task.TaskStatus.Done,
                      TaskPriority = 1,
                      TaskDescription = "Implement restful.",
                      ProjectId = 2
                  });
               modelBuilder.Entity<Task>().HasData(
                new Task()
                {
                    Id = 3,
                    TaskName = "FrontEnd Implementation",
                    Status = Models.Task.TaskStatus.ToDo,
                    TaskPriority = 2,
                    TaskDescription = "Implement front-end.",
                    ProjectId = 3
                });
        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
