using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Management_API.Migrations
{
    public partial class dependencyIdupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskDependency",
                table: "TaskDependency");

            migrationBuilder.AddColumn<int>(
                name: "DependencyId",
                table: "TaskDependency",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskDependency",
                table: "TaskDependency",
                column: "DependencyId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDependency_TaskItemId",
                table: "TaskDependency",
                column: "TaskItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskDependency",
                table: "TaskDependency");

            migrationBuilder.DropIndex(
                name: "IX_TaskDependency_TaskItemId",
                table: "TaskDependency");

            migrationBuilder.DropColumn(
                name: "DependencyId",
                table: "TaskDependency");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskDependency",
                table: "TaskDependency",
                columns: new[] { "TaskItemId", "DependentTaskItemId" });
        }
    }
}
