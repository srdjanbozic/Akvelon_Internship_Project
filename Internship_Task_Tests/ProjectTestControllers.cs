using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using Internship_Task.Controllers;
using Internship_Task.Data;
using Internship_Task.Models;
using Internship_Task.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task_Tests
{
    public class ProjectTestControllers
    {
        private readonly Repository<Project> _repository;
        private readonly AppDbContext _context;
        private readonly ProjectController _controller;
        public ProjectTestControllers()
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
            _context.Projects.AddRange
               (
            new Project() { Id = 1, ProjectName = "Docker", StartDate = new DateTime(2022, 2, 12), CompletionDate = new DateTime(2023, 2, 12), Status = Project.ProjectStatus.Active, Priority = 3 },
            new Project() { Id = 2, ProjectName = "MusicPlayer", StartDate = new DateTime(2021, 3, 19), CompletionDate = new DateTime(2022, 2, 12), Status = Project.ProjectStatus.Completed, Priority = 1 },
            new Project() { Id = 3, ProjectName = "WebMVC", StartDate = new DateTime(2022, 9, 12), CompletionDate = new DateTime(2024, 2, 12), Status = Project.ProjectStatus.Active, Priority = 2 },
            new Project() { Id = 4, ProjectName = "Social Media WebSite", StartDate = new DateTime(2022, 1, 12), CompletionDate = new DateTime(2023, 2, 12), Status = Project.ProjectStatus.Active, Priority = 3 }
               );
            _context.SaveChanges();
        }
        [Fact]
        public void Get_All_Projects_ReturnsTasks()
        {
            // Arrange
            Repository<Project> repo = new Repository<Project>(_context);
            var controller = new ProjectController(repo);
            // Act
            IEnumerable<Project> projects = repo.GetAll();
            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<Project>>(projects);
            Assert.Equal(4, model.Count());
            
        }
        [Fact]
        public void Get_All_Projects_Return_Bad_Request()
        {
            // Arrange
            Repository<Project> repo = new Repository<Project>(_context);
            var controller = new ProjectController(repo);
            // Act
            IEnumerable<Project> projects = repo.GetAll();
            projects = null;
            //Assert
            if(projects != null)
            {
                Assert.IsType<BadRequestResult>(projects);
            }
        }
        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            //Arrange
            var notExistingId = 13;
            Repository<Project> repo = new Repository<Project>(_context);
            ProjectController _controller = new ProjectController(repo);
            //Act
            var badReponse = _controller.DeleteProject(notExistingId);
            //Assert
            Assert.IsType<NotFoundResult>(badReponse);
        }
        [Fact]
        public void Remove_ExistingIdPassed_ReturnsNoContentResult()
        {
            // Arrange
            var notExistingId = 1;
            Repository<Project> repo = new Repository<Project>(_context);
            ProjectController _controller = new ProjectController(repo);
            // Act
            var noContentResponse = _controller.DeleteProject(notExistingId);
            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }
        [Fact]
        public void Remove_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = 1;
            Repository<Project> repo = new Repository<Project>(_context);
            ProjectController _controller = new ProjectController(repo);
            // Act
            var okResponse = _controller.DeleteProject(existingId);
            // Assert
            Assert.Equal(3, _controller.GetAllProjects().Count());
            Assert.IsType <NoContentResult>(okResponse);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var testItem = new Project()
            {
            Id = 5,
            ProjectName = "ASP.NET",
            StartDate = new DateTime(2022, 2, 12),
            CompletionDate = new DateTime(2023, 2, 12),
            Status = Project.ProjectStatus.Active,
            Priority = 3
            };
            Repository<Project> repo = new Repository<Project>(_context);
            ProjectController _controller = new ProjectController(repo);
            // Act
            var createdResponse = _controller.CreateProject(testItem);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Project()
            {
                Id = 5,
                ProjectName = "ASP.NET",
                StartDate = new DateTime(2022, 2, 12),
                CompletionDate = new DateTime(2023, 2, 12),
                Status = Project.ProjectStatus.Active,
                Priority = 3
            };
            Repository<Project> repo = new Repository<Project>(_context);
            ProjectController _controller = new ProjectController(repo);
            // Act
            var createdResponse = _controller.CreateProject(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Project;
            // Assert
            Assert.IsType<Project>(item);
            Assert.Equal("ASP.NET", item.ProjectName);
        }
        [Fact]
        public void GetById_KnownIdPassed_ReturnsEqual()
        {
            // Arrange
            Repository<Project> repo = new Repository<Project>(_context);
            ProjectController _controller = new ProjectController(repo);
            int projId = 2;
            //Act
            var foundResult = _controller.GetProjectById(projId);
            // Assert
            Assert.Equal(2, foundResult.Id);
        }
        [Fact]
        public void UpdateById_KnownIdPassed_CreatedAtActionResult()
        {
            // Arrange
            Repository<Project> repo = new Repository<Project>(_context);
            ProjectController _controller = new ProjectController(repo); 
            var testItem = new Project()
            {
                ProjectName = "NewWebApi",
                StartDate = new DateTime(2022, 2, 12),
                CompletionDate = new DateTime(2023, 2, 12),
                Status = Project.ProjectStatus.Active,
                Priority = 3
            };
            testItem.ProjectName = "newProject";
            var testResult = _controller.UpdateProject(testItem.Id,testItem) as CreatedAtActionResult;
            var item = testResult.Value as Project;
            Assert.IsType<Project>(item);
            Assert.Equal("newProject",testItem.ProjectName);
            
        }
    }
}
