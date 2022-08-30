using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship_Task.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TaskPriority = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_task_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "project",
                columns: new[] { "ProjectId", "CompletionDate", "Priority", "ProjectName", "StartDate", "Status" },
                values: new object[] { 1, new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Machine Learning", new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "project",
                columns: new[] { "ProjectId", "CompletionDate", "Priority", "ProjectName", "StartDate", "Status" },
                values: new object[] { 2, new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "EShop", new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.InsertData(
                table: "project",
                columns: new[] { "ProjectId", "CompletionDate", "Priority", "ProjectName", "StartDate", "Status" },
                values: new object[] { 3, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Front-End", new DateTime(2022, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "task",
                columns: new[] { "TaskId", "ProjectId", "Status", "TaskDescription", "TaskName", "TaskPriority" },
                values: new object[] { 1, 1, 2, "Implement merge sorting algorithm.", "Sorting", 3 });

            migrationBuilder.InsertData(
                table: "task",
                columns: new[] { "TaskId", "ProjectId", "Status", "TaskDescription", "TaskName", "TaskPriority" },
                values: new object[] { 2, 2, 3, "Implement restful.", "RestFul Services", 1 });

            migrationBuilder.InsertData(
                table: "task",
                columns: new[] { "TaskId", "ProjectId", "Status", "TaskDescription", "TaskName", "TaskPriority" },
                values: new object[] { 3, 3, 1, "Implement front-end.", "FrontEnd Implementation", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_task_ProjectId",
                table: "task",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropTable(
                name: "project");
        }
    }
}
