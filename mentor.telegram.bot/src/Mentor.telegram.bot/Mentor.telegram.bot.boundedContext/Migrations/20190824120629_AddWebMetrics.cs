using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mentor.bot.boundedContext.Migrations
{
    public partial class AddWebMetrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatLiveUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 35, nullable: true),
                    Email = table.Column<string>(maxLength: 35, nullable: true),
                    Auth = table.Column<string>(maxLength: 32, nullable: true),
                    BotId = table.Column<string>(maxLength: 64, nullable: true),
                    ChannelId = table.Column<string>(maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLiveUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 35, nullable: true),
                    Username = table.Column<string>(nullable: true),
                    BotId = table.Column<string>(maxLength: 64, nullable: true),
                    DataJson = table.Column<string>(nullable: true),
                    ChannelId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Cookies = table.Column<string>(maxLength: 5, nullable: true),
                    BrowserLanguage = table.Column<string>(maxLength: 256, nullable: true),
                    Language = table.Column<string>(maxLength: 8, nullable: true),
                    Platform = table.Column<string>(maxLength: 8, nullable: true),
                    Connection = table.Column<string>(maxLength: 256, nullable: true),
                    UserAgent = table.Column<string>(maxLength: 256, nullable: true),
                    Webdriver = table.Column<string>(maxLength: 256, nullable: true),
                    Geolocation = table.Column<string>(nullable: true),
                    CurrentUrl = table.Column<string>(nullable: true),
                    BotId = table.Column<string>(maxLength: 64, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebMetrics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatLiveUsers");

            migrationBuilder.DropTable(
                name: "EventItems");

            migrationBuilder.DropTable(
                name: "WebMetrics");
        }
    }
}
