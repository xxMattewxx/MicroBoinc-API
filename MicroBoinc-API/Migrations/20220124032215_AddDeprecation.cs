using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroBoincAPI.Migrations
{
    public partial class AddDeprecation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsBinaries_Projects_ProjectID",
                table: "ProjectsBinaries");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectID",
                table: "ProjectsBinaries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deprecated",
                table: "ProjectsBinaries",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsBinaries_Projects_ProjectID",
                table: "ProjectsBinaries",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsBinaries_Projects_ProjectID",
                table: "ProjectsBinaries");

            migrationBuilder.DropColumn(
                name: "Deprecated",
                table: "ProjectsBinaries");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectID",
                table: "ProjectsBinaries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsBinaries_Projects_ProjectID",
                table: "ProjectsBinaries",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
