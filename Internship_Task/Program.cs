using Internship_Task.Data;
using Internship_Task.Models;
using Internship_Task.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Configuration;
using Task = Internship_Task.Models.Task;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Akvelon Internship Project Task",
            Description = "ASP.NET Core Web API project",
            Contact = new OpenApiContact
            {
                Name = " Linkedin Profile",
                Url = new Uri("https://www.linkedin.com/in/srdjan-bozic-b560b6231/")
            },
            License = new OpenApiLicense
            {
                Name = "License = GNU General Public License v2.0",
                Url = new Uri("https://github.com/srdjanbozic/Internship_Tasks/blob/Test_Task_Akvelon_Internship/LICENSE")
            }
        });
        c.EnableAnnotations();
    });

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registering repository services and mapping them to our models
#region Repositories
// Whenever these interfaces are requested - return the mapped class
//builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
//builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IRepository<Project>, Repository<Project>>();
builder.Services.AddScoped<IRepository<Task>, Repository<Task>>();
#endregion
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Internship_Task v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
