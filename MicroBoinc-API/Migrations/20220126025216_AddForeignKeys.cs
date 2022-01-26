using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroBoincAPI.Migrations
{
    public partial class AddForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Assignments_AssignmentID",
                table: "Results");

            migrationBuilder.AlterColumn<long>(
                name: "AssignmentID",
                table: "Results",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Status",
                table: "Tasks",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Assignments_AssignmentID",
                table: "Results",
                column: "AssignmentID",
                principalTable: "Assignments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Assignments_AssignmentID",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Status",
                table: "Tasks");

            migrationBuilder.AlterColumn<long>(
                name: "AssignmentID",
                table: "Results",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Assignments_AssignmentID",
                table: "Results",
                column: "AssignmentID",
                principalTable: "Assignments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
