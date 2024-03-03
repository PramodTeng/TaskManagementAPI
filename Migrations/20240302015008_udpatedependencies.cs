using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Management_API.Migrations
{
    public partial class udpatedependencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependency_Tasks_TaskItemId",
                table: "TaskDependency");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependency_Tasks_TaskItemId",
                table: "TaskDependency",
                column: "TaskItemId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependency_Tasks_TaskItemId",
                table: "TaskDependency");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependency_Tasks_TaskItemId",
                table: "TaskDependency",
                column: "TaskItemId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
