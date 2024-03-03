using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Management_API.Migrations
{
    public partial class RecurrenceUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Interval",
                table: "Tasks",
                newName: "TaskType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskType",
                table: "Tasks",
                newName: "Interval");
        }
    }
}
