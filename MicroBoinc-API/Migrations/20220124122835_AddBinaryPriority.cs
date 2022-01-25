using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroBoincAPI.Migrations
{
    public partial class AddBinaryPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "ProjectsBinaries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ProjectsBinaries");
        }
    }
}
