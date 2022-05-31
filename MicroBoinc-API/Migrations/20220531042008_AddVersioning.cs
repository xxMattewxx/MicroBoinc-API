using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MicroBoincAPI.Migrations
{
    public partial class AddVersioning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsKeys_Accounts_AccountID",
                table: "AccountsKeys");

            migrationBuilder.DropIndex(
                name: "IX_LeaderboardsEntries_KeyID",
                table: "LeaderboardsEntries");

            migrationBuilder.AlterColumn<long>(
                name: "AccountID",
                table: "AccountsKeys",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ClientsVersions",
                columns: table => new
                {
                    Version = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BinaryID = table.Column<long>(type: "bigint", nullable: true),
                    Codename = table.Column<string>(type: "text", nullable: true),
                    FriendlyName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsVersions", x => x.Version);
                    table.ForeignKey(
                        name: "FK_ClientsVersions_Binaries_BinaryID",
                        column: x => x.BinaryID,
                        principalTable: "Binaries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardsEntries_KeyID_ProjectID",
                table: "LeaderboardsEntries",
                columns: new[] { "KeyID", "ProjectID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientsVersions_BinaryID",
                table: "ClientsVersions",
                column: "BinaryID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsKeys_Accounts_AccountID",
                table: "AccountsKeys",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsKeys_Accounts_AccountID",
                table: "AccountsKeys");

            migrationBuilder.DropTable(
                name: "ClientsVersions");

            migrationBuilder.DropIndex(
                name: "IX_LeaderboardsEntries_KeyID_ProjectID",
                table: "LeaderboardsEntries");

            migrationBuilder.AlterColumn<long>(
                name: "AccountID",
                table: "AccountsKeys",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardsEntries_KeyID",
                table: "LeaderboardsEntries",
                column: "KeyID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsKeys_Accounts_AccountID",
                table: "AccountsKeys",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
