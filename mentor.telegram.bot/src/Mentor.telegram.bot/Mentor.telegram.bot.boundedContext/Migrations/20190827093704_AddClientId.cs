using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mentor.bot.boundedContext.Migrations
{
    public partial class AddClientId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "EventItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "EventItems");
        }
    }
}
