using Microsoft.EntityFrameworkCore.Migrations;

namespace Mentor.bot.boundedContext.Migrations
{
    public partial class AddWebMetricsCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Geolocation",
                table: "WebMetrics");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "WebMetrics",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "WebMetrics",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "WebMetrics",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Loc",
                table: "WebMetrics",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postal",
                table: "WebMetrics",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "WebMetrics",
                maxLength: 16,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "WebMetrics");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "WebMetrics");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "WebMetrics");

            migrationBuilder.DropColumn(
                name: "Loc",
                table: "WebMetrics");

            migrationBuilder.DropColumn(
                name: "Postal",
                table: "WebMetrics");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "WebMetrics");

            migrationBuilder.AddColumn<string>(
                name: "Geolocation",
                table: "WebMetrics",
                nullable: true);
        }
    }
}
