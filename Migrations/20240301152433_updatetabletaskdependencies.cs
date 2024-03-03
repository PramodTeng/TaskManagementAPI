using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Management_API.Migrations
{
    public partial class updatetabletaskdependencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependency_Tasks_DependentTaskItemId",
                table: "TaskDependency");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependency_Tasks_TaskItemId",
                table: "TaskDependency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskDependency",
                table: "TaskDependency");

            migrationBuilder.RenameTable(
                name: "TaskDependency",
                newName: "TaskDependencies");

            migrationBuilder.RenameIndex(
                name: "IX_TaskDependency_DependentTaskItemId",
                table: "TaskDependencies",
                newName: "IX_TaskDependencies_DependentTaskItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskDependencies",
                table: "TaskDependencies",
                columns: new[] { "TaskItemId", "DependentTaskItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependencies_Tasks_DependentTaskItemId",
                table: "TaskDependencies",
                column: "DependentTaskItemId",
                principalTable: "Tasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependencies_Tasks_TaskItemId",
                table: "TaskDependencies",
                column: "TaskItemId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependencies_Tasks_DependentTaskItemId",
                table: "TaskDependencies");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependencies_Tasks_TaskItemId",
                table: "TaskDependencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskDependencies",
                table: "TaskDependencies");

            migrationBuilder.RenameTable(
                name: "TaskDependencies",
                newName: "TaskDependency");

            migrationBuilder.RenameIndex(
                name: "IX_TaskDependencies_DependentTaskItemId",
                table: "TaskDependency",
                newName: "IX_TaskDependency_DependentTaskItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskDependency",
                table: "TaskDependency",
                columns: new[] { "TaskItemId", "DependentTaskItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependency_Tasks_DependentTaskItemId",
                table: "TaskDependency",
                column: "DependentTaskItemId",
                principalTable: "Tasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependency_Tasks_TaskItemId",
                table: "TaskDependency",
                column: "TaskItemId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
