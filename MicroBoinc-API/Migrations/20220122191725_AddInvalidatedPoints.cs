using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MicroBoincAPI.Migrations
{
    public partial class AddInvalidatedPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsBinaries_Platforms_PlatformID",
                table: "ProjectsBinaries");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Tasks",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AlterColumn<long>(
                name: "PlatformID",
                table: "ProjectsBinaries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Accounts",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateTable(
                name: "LeaderboardsEntries",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalPoints = table.Column<long>(type: "bigint", nullable: false),
                    ValidatedPoints = table.Column<long>(type: "bigint", nullable: false),
                    InvalidatedPoints = table.Column<long>(type: "bigint", nullable: false),
                    AccountID = table.Column<long>(type: "bigint", nullable: false),
                    KeyID = table.Column<long>(type: "bigint", nullable: false),
                    ProjectID = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderboardsEntries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeaderboardsEntries_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaderboardsEntries_AccountsKeys_KeyID",
                        column: x => x.KeyID,
                        principalTable: "AccountsKeys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaderboardsEntries_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaderboardsSnapshots",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalPoints = table.Column<long>(type: "bigint", nullable: false),
                    ValidatedPoints = table.Column<long>(type: "bigint", nullable: false),
                    SnapshotTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    InvalidatedPoints = table.Column<long>(type: "bigint", nullable: false),
                    ExecutedAction = table.Column<int>(type: "integer", nullable: false),
                    AccountID = table.Column<long>(type: "bigint", nullable: false),
                    KeyID = table.Column<long>(type: "bigint", nullable: false),
                    ProjectID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderboardsSnapshots", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeaderboardsSnapshots_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaderboardsSnapshots_AccountsKeys_KeyID",
                        column: x => x.KeyID,
                        principalTable: "AccountsKeys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaderboardsSnapshots_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardsEntries_AccountID",
                table: "LeaderboardsEntries",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardsEntries_KeyID",
                table: "LeaderboardsEntries",
                column: "KeyID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardsEntries_ProjectID",
                table: "LeaderboardsEntries",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardsSnapshots_AccountID",
                table: "LeaderboardsSnapshots",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardsSnapshots_KeyID",
                table: "LeaderboardsSnapshots",
                column: "KeyID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardsSnapshots_ProjectID",
                table: "LeaderboardsSnapshots",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsBinaries_Platforms_PlatformID",
                table: "ProjectsBinaries",
                column: "PlatformID",
                principalTable: "Platforms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsBinaries_Platforms_PlatformID",
                table: "ProjectsBinaries");

            migrationBuilder.DropTable(
                name: "LeaderboardsEntries");

            migrationBuilder.DropTable(
                name: "LeaderboardsSnapshots");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Accounts");

            migrationBuilder.AlterColumn<long>(
                name: "PlatformID",
                table: "ProjectsBinaries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsBinaries_Platforms_PlatformID",
                table: "ProjectsBinaries",
                column: "PlatformID",
                principalTable: "Platforms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
