using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Management_API.Migrations
{
    public partial class UpdateTaskItemWithDependencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskDependency",
                columns: table => new
                {
                    TaskItemId = table.Column<int>(type: "int", nullable: false),
                    DependentTaskItemId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDependency", x => new { x.TaskItemId, x.DependentTaskItemId });
                    table.ForeignKey(
                        name: "FK_TaskDependency_Tasks_DependentTaskItemId",
                        column: x => x.DependentTaskItemId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskDependency_Tasks_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDependency_DependentTaskItemId",
                table: "TaskDependency",
                column: "DependentTaskItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskDependency");
        }
    }
}
