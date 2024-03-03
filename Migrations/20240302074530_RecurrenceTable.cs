using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Management_API.Migrations
{
    public partial class RecurrenceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Interval",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Recurrence",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Recurrence",
                table: "Tasks");
        }
    }
}
