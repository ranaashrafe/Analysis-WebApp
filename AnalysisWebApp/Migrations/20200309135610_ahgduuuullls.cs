using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnalysisWebApp.Migrations
{
    public partial class ahgduuuullls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageBody = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    OrganDetailID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Organs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    AddedByUserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Users_AddedByUserID",
                        column: x => x.AddedByUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shape = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OrganID = table.Column<int>(nullable: false),
                    MessageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrganDetails_Organs_OrganID",
                        column: x => x.OrganID,
                        principalTable: "Organs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChoices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    IsFav = table.Column<bool>(nullable: false),
                    OrganDetailID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChoices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserChoices_OrganDetails_OrganDetailID",
                        column: x => x.OrganDetailID,
                        principalTable: "OrganDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChoices_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganDetails_OrganID",
                table: "OrganDetails",
                column: "OrganID");

            migrationBuilder.CreateIndex(
                name: "IX_UserChoices_OrganDetailID",
                table: "UserChoices",
                column: "OrganDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_UserChoices_UserID",
                table: "UserChoices",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddedByUserID",
                table: "Users",
                column: "AddedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserChoices");

            migrationBuilder.DropTable(
                name: "OrganDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organs");
        }
    }
}
