using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalR_Chat.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserTypeId = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    UserSenderId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserReceiverId = table.Column<int>(type: "INTEGER", nullable: false),
                    SendDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_User_UserReceiverId",
                        column: x => x.UserReceiverId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_User_UserSenderId",
                        column: x => x.UserSenderId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "System" });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "User" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "Name", "UserTypeId" },
                values: new object[] { 1, new DateTime(1992, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sistema", 2 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "Name", "UserTypeId" },
                values: new object[] { 2, new DateTime(1995, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mathieu", 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "Name", "UserTypeId" },
                values: new object[] { 3, new DateTime(1998, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "José", 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "Name", "UserTypeId" },
                values: new object[] { 4, new DateTime(1994, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jurassic Park", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserReceiverId",
                table: "Message",
                column: "UserReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserSenderId",
                table: "Message",
                column: "UserSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeId",
                table: "User",
                column: "UserTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
