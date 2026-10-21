using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BotCreator.Infrastructure.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Avatar = table.Column<string>(maxLength: 500, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    BotTemplateId = table.Column<int>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeployed = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bots_BotTemplates",
                        column: x => x.BotTemplateId,
                        principalTable: "BotTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bots_Users",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bots_BotTemplateId",
                table: "Bots",
                column: "BotTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_OwnerId",
                table: "Bots",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BotTemplates",
                table: "BotTemplates",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bots");

            migrationBuilder.DropTable(
                name: "BotTemplates");
        }
    }
}
