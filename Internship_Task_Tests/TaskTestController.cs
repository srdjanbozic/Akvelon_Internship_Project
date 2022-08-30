
using Internship_Task.Repositories;
using System.Collections.Generic;
using Moq;
using Xunit;
using Internship_Task.Models;
using Internship_Task.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Internship_Task.Controllers;
using Task = Internship_Task.Models.Task;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;

namespace Internship_Task_Tests
{
    public class TaskTestController 
    {
        private Repository<Task> _repository;
        private readonly AppDbContext _context;
        public TaskTestController()
        {
            // By supplying a new service provider for each context
            // we have a single database instance per case.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            // Build context options
            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectDB")
                .UseInternalServiceProvider(serviceProvider);
            // Instantiate the context
            _context = new AppDbContext(builder.Options);
            // Seed the database
            _context.Tasks.AddRange(
                new Task() { Id = 1, TaskName = "Integrations", Status = Task.TaskStatus.ToDo, TaskPriority = 1, TaskDescription = "Implement", ProjectId = 1 },
                new Task() { Id = 2, TaskName = "UnitTesting", Status = Task.TaskStatus.InProgress, TaskPriority = 5, TaskDescription = "UnitTesting of web api", ProjectId = 2 },
                new Task() { Id = 3, TaskName = "Configure Kubernetes", Status = Task.TaskStatus.Done, TaskPriority = 1, TaskDescription = "Configure", ProjectId = 3 },
                new Task() { Id = 4, TaskName = "Sorting", Status = Task.TaskStatus.ToDo, TaskPriority = 3, TaskDescription = "Implement", ProjectId = 2 }
               );
            _context.SaveChanges();
        }
        [Fact]
        public void Get_All_Task_ReturnsTasks()
        {
            // Arrange
            Repository<Task> repo = new Repository<Task>(_context);
            var controller = new TaskController(repo);
            // Act
            IEnumerable<Task> tasks = repo.GetAll();
            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<Task>>(tasks);
            Assert.Equal(4, model.Count());
        }
        [Fact]
        public void Get_All_Tasks_Return_Number_Of_Tasks_Not_Equal()
        {
            // Arrange
            Repository<Task> repo = new Repository<Task>(_context);
            var controller = new TaskController(repo);
            // Act
            IEnumerable<Task> tasks = repo.GetAll();
            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<Task>>(tasks);
            Assert.NotEqual(3, model.Count());
        }
        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            var notExistingGuid = 13;
            Repository<Task> repo = new Repository<Task>(_context);
            TaskController _controler = new TaskController(repo);
            var badReponse = _controler.DeleteTask(notExistingGuid);
            Assert.IsType<NotFoundResult>(badReponse);
        }
        [Fact]
        public void Remove_ExistingIdPassed_ReturnsNoContentResult()
        {
            // Arrange
            var notExistingId = 2;
            Repository<Task> repo = new Repository<Task>(_context);
            TaskController _controller = new TaskController(repo);
            // Act
            var noContentResponse = _controller.DeleteTask(notExistingId);
            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var testItem = new Task()
            {
                Id = 5,
                TaskName = "Sorting",
                Status = Task.TaskStatus.ToDo,
                TaskPriority = 3,
                TaskDescription = "Task to do.",
                ProjectId = 1
            };
            Repository<Task> repo = new Repository<Task>(_context);
            TaskController _controller = new TaskController(repo);
            // Act
            var createdResponse = _controller.CreateTask(testItem);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Task()
            {
                Id = 5,
                TaskName = "Sorting",
                Status = Task.TaskStatus.ToDo,
                TaskPriority = 3,
                TaskDescription = "Task to do.",
                ProjectId = 1
            };
            Repository<Task> repo = new Repository<Task>(_context);
            TaskController _controller = new TaskController(repo);
            // Act
            var createdResponse = _controller.CreateTask(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Task;
            // Assert
            Assert.IsType<Task>(item);
            Assert.Equal("Sorting", item.TaskName);
        }
    }
}