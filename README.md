
# Akvelon Internship Test Task


This is a WEB API project built with purpose of asserting my skills in .NET Core and Entity Framework Core.


## Description
My WEB API project keeps track of the tasks and projects. Each project can contain multiple tasks, but each task can be part of one, and only one project.

Each task has the following fields listed bellow:

- Id
- Task name
- Task status (Enum: NotStarted, Active, Completed)
- Task priority
- Task description

Each project has the following fields listed bellow:

- Id
- Project name
- Stard date
- Completion date
- Project status (Enum: ToDo, In Progress, Completed)
- Priority

Functional capabilities of the project
-
- Ability to create/ view / modify / remove projects - Basic CRUD operations
- Ability to create/ view / modify / remove tasks - Basic CRUD operations
- Ability to add and remove tasks from a project 
- Ability to view all tasks in the project

Non functional requirments
-
- The project was built using a three-layer architecture - Data Access Layer, Logic Layer and Representation Layer.
- The layers are linked by dependency injection.
- The project is completed using design patterns such as the repository pattern, which provides abstraction over the data layer.
- Data annotations are used to validate models.
- Code First Approach for mapping entity models into tabels in database - ORM
- Unit tests are used to validate the entire logic.

Techonology
-
- .NET 6 (Version 6.0.8)
- Entity Framework Core (Version 6.4.4)
- Microsoft SQL Server Management Studio 18
- Swagger Ui for automated API documentation
- Docker

Other installed dependencies
-
- Entity Framework Core Tools (Version 6.0.8)
- Entity Framework (Version 6.4.4)
- Entity Framework Core SqlServer (Version 6.0.8)
- Entity Framework Extensions Dependency Injection (Version 6.0.0)
- Swashbuckle AspNet Core (Version 6.4.0)
- Swashbuckle AspNet Core Annotations (Version 6.4.0)
- Fluent Assertions (Version 6.7.0)
- Microsoft Entity Framework Core InMemory (Version 6.0.8)
- Microsoft .Net Test Sdk (Version 17.3.0)
- Moq (Version 4.18.2)
- Xunit (Version 2.4.2)
- Xunit Runner Visual Studio (Version 2.4.5)






## Titles
 - Internship_Task
 - Internship_Task_Tests

## Installation
In order to run my Web Api project, you have to follow next steps:
-
- Before getting the code, check if you have installed Visual Studio 2022 (https://visualstudio.microsoft.com/vs/) and Microsoft SQL Server Management Studio 18 (https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
- Download zip file from Github (by clicking on green button "Code") or clone it directly into IDE.
- Open project in Visual studio 2022
- Make sure you install all the latest versions of dependencies - Right click on 'Solution:Internship_Task' -> Manage NuGet Packages -> Install latest versions of all dependencies I mentioned above
```bash
- Entity Framework Core Tools (Version 6.0.8)
- Entity Framework (Version 6.4.4)
- Entity Framework Core SqlServer (Version 6.0.8)
- Entity Framework Extensions Dependency Injection (Version 6.0.0)
- Swashbuckle AspNet Core (Version 6.4.0)
- Swashbuckle AspNet Core Annotations (Version 6.4.0)
- Fluent Assertions (Version 6.7.0)
- Microsoft Entity Framework Core InMemory (Version 6.0.8)
- Microsoft .Net Test Sdk (Version 17.3.0)
- Moq (Version 4.18.2)
- Xunit (Version 2.4.2)
- Xunit Runner Visual Studio (Version 2.4.5)
```
Check if you have installed MSSQL 18 (https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

If you have installed MSSQL 18, you can add Migrations using Object Relational Mapping via Entity Framework.
Below are the steps on adding the initial migration to create the database:
-
- Once you have opened the solution, in the lower end of VisualStudio, click on Package Manager Console
- For "Default Project" choose the option Internship_Task
- Type the following: Add-Migration CreateDatabase -o Data/Migrations
- When the migration is added, type Update-Database, and you can open the MSSMS to check if the database has appeared
When the database is set up, you can run project - It will open Swagger using
-

## API Reference

#### Get all projects

```http
  GET /api/Project
```

This endpoint returns all projects from the database.
-


#### Get project by Id

```http
  GET /api/Project/${id}
```
This endpoint returns a single project with specified Id.
-
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of project to fetch |

#### Post project


```http
  POST /api/Project
```
This endpoint creates a new project in the database.
-

#### Update project
```http
  PUT /api/Project/ ${id}
```
This endpoint updates a single project with specified Id in the database.

#### Remove project
```http
  DELETE /api/Project/ ${id}
```
This endpoint removes a single project with specified Id from the database.

#### Get all tasks

```http
  GET /api/Task
```

This endpoint returns all tasks from the database.
-
#### Get task by Id

```http
  GET /api/Task/${id}
```
This endpoint returns a single task with specified Id.
-
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of task to fetch |

#### Post project


```http
  POST /api/Task
```
This endpoint creates a new task in the database.
-

#### Update task
```http
  PUT /api/Task/ ${id}
```
This endpoint updates a single task with specified Id in the database.

#### Remove task
```http
  DELETE /api/Task/ ${id}
```
This endpoint removes a single task with specified Id from the database.


## Unit Tests
- I used the Xunit framework to test the logic of my application.
- To test this project, I had to mock certain functionalities as well as use an in memory database to mock the database context.
Project Controller Tests are as following:
- 
- Get_All_Projects_ReturnsTasks - should return all projects from db and check if number of project is right
- Remove_NotExistingIdPassed_ReturnsNotFoundResponse - based on non existing Id should return status code 404 Not Found Result
- Remove_ExistingIdPassed_ReturnsNoContentResult - based on existing Id, should remove specified project and return No Content status code (204)
- Remove_ExistingIdPassed_RemovesOneItem - remove project with specified Id and check the number of projects in db after Remove_ExistingIdPassed_ReturnsNoContentResult
- Add_ValidObjectPassed_ReturnsCreatedResponse - add new project in db and returns 201 Created at Action status code
- Add_ValidObjectPassed_ReturnedResponseHasCreatedItem - create a new project and check value property and type of an object
- GetById_UnknownGuidPassed_ReturnsNotEqual - based on non existing Id, should return not found status code
- GetById_UnknownIdPassed_ReturnsEqual - based on existing Id, should return status code No Content and check if Id is equal to specified Id
- UpdateById_KnownIdPassed_CreatedAtActionResult - create new object, update specified field and check if filed is UpdateById_KnownIdPassed_CreatedAtActionResult
Task Controller Tests are as following :
-
- Get_All_Task_ReturnsTasks - should return all tesks from db and check if number of tesk is right
- Remove_NotExistingGuidPassed_ReturnsNotFoundResponse - based on non existing Id should return status code 404 Not Found Result
- Remove_ExistingIdPassed_ReturnsNoContentResult - based on existing Id, should remove specified Task and return No Content status code (204)
- Add_ValidObjectPassed_ReturnsCreatedResponse - add new Task in db and returns 201 Created at Action status code
- Add_ValidObjectPassed_ReturnedResponseHasCreatedItem - create a new project and check value property and type of an object

## Support

For support, email srdjanbozic168@gmail.com or add me on linkedin https://www.linkedin.com/in/srdjan-bozic-b560b6231/.

