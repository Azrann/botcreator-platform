using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mentor.bot.boundedContext.Migrations
{
    public partial class feedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WebUserId = table.Column<Guid>(nullable: false),
                    Platform = table.Column<string>(maxLength: 8, nullable: true),
                    UserAgent = table.Column<string>(maxLength: 256, nullable: true),
                    CurrentUrl = table.Column<string>(nullable: true),
                    BotId = table.Column<string>(maxLength: 64, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 32, nullable: true),
                    ReplyToId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");
        }
    }
}
