using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MicroBoincAPI.Migrations
{
    public partial class AddInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Offset = table.Column<long>(type: "bigint", nullable: false),
                    DiscordID = table.Column<long>(type: "bigint", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Binaries",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Checksum = table.Column<string>(type: "text", nullable: true),
                    DownloadURL = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Binaries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AccountsKeys",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Identifier = table.Column<string>(type: "text", nullable: true),
                    IsWeak = table.Column<bool>(type: "boolean", nullable: false),
                    IsRoot = table.Column<bool>(type: "boolean", nullable: false),
                    AccountID = table.Column<long>(type: "bigint", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsKeys", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountsKeys_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    OwnedByID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Groups_Accounts_OwnedByID",
                        column: x => x.OwnedByID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsSpecialized = table.Column<bool>(type: "boolean", nullable: false),
                    DetectorBinaryID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Platforms_Binaries_DetectorBinaryID",
                        column: x => x.DetectorBinaryID,
                        principalTable: "Binaries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupsPermissions",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupID = table.Column<long>(type: "bigint", nullable: true),
                    AccountID = table.Column<long>(type: "bigint", nullable: true),
                    Permissions = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsPermissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupsPermissions_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupsPermissions_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupID = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    AddedByID = table.Column<long>(type: "bigint", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    InputServerURL = table.Column<string>(type: "text", nullable: true),
                    UseExternalInputServer = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Projects_Accounts_AddedByID",
                        column: x => x.AddedByID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsBinaries",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BinaryID = table.Column<long>(type: "bigint", nullable: true),
                    ProjectID = table.Column<long>(type: "bigint", nullable: true),
                    PlatformID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsBinaries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectsBinaries_Binaries_BinaryID",
                        column: x => x.BinaryID,
                        principalTable: "Binaries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectsBinaries_Platforms_PlatformID",
                        column: x => x.PlatformID,
                        principalTable: "Platforms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectsBinaries_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupID = table.Column<long>(type: "bigint", nullable: true),
                    InputID = table.Column<string>(type: "text", nullable: true),
                    ProjectID = table.Column<long>(type: "bigint", nullable: true),
                    InputData = table.Column<string>(type: "text", nullable: true),
                    SlotsLeft = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ResultsLeft = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tasks_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskID = table.Column<long>(type: "bigint", nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AssignedToID = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assignments_AccountsKeys_AssignedToID",
                        column: x => x.AssignedToID,
                        principalTable: "AccountsKeys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignments_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskID = table.Column<long>(type: "bigint", nullable: true),
                    StdErr = table.Column<string>(type: "text", nullable: true),
                    StdOut = table.Column<string>(type: "text", nullable: true),
                    ExitCode = table.Column<short>(type: "smallint", nullable: false),
                    ProjectID = table.Column<long>(type: "bigint", nullable: true),
                    ExecutionTime = table.Column<long>(type: "bigint", nullable: false),
                    AssignmentID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Results_Assignments_AssignmentID",
                        column: x => x.AssignmentID,
                        principalTable: "Assignments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountsKeys_AccountID",
                table: "AccountsKeys",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssignedToID",
                table: "Assignments",
                column: "AssignedToID");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_TaskID",
                table: "Assignments",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OwnedByID",
                table: "Groups",
                column: "OwnedByID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsPermissions_AccountID",
                table: "GroupsPermissions",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsPermissions_GroupID",
                table: "GroupsPermissions",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_DetectorBinaryID",
                table: "Platforms",
                column: "DetectorBinaryID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AddedByID",
                table: "Projects",
                column: "AddedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_GroupID",
                table: "Projects",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsBinaries_BinaryID",
                table: "ProjectsBinaries",
                column: "BinaryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsBinaries_PlatformID",
                table: "ProjectsBinaries",
                column: "PlatformID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsBinaries_ProjectID",
                table: "ProjectsBinaries",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_AssignmentID",
                table: "Results",
                column: "AssignmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ProjectID",
                table: "Results",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_TaskID",
                table: "Results",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_GroupID",
                table: "Tasks",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ID_SlotsLeft",
                table: "Tasks",
                columns: new[] { "ID", "SlotsLeft" });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectID",
                table: "Tasks",
                column: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsPermissions");

            migrationBuilder.DropTable(
                name: "ProjectsBinaries");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Binaries");

            migrationBuilder.DropTable(
                name: "AccountsKeys");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
