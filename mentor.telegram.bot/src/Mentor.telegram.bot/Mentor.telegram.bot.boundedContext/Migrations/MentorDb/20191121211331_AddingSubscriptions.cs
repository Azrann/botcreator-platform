using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mentor.bot.boundedContext.Migrations.MentorDb
{
    public partial class AddingSubscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    ChatId = table.Column<long>(nullable: false),
                    BotId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Provider = table.Column<int>(nullable: false),
                    PaymentStatus = table.Column<int>(nullable: false),
                    OriginalAmount = table.Column<decimal>(type: "decimal(15,8)", nullable: false),
                    OriginalCurrency = table.Column<string>(maxLength: 5, nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    SubscriptionMonths = table.Column<short>(nullable: false),
                    SubscriptionDays = table.Column<short>(nullable: false),
                    TransactionId = table.Column<string>(nullable: true),
                    BuyerCurrency = table.Column<string>(maxLength: 5, nullable: true),
                    BuyerAmount = table.Column<decimal>(type: "decimal(15,8)", nullable: false),
                    ReceivedAmount = table.Column<decimal>(type: "decimal(15,8)", nullable: false),
                    ReceivedConfirms = table.Column<int>(nullable: false),
                    CryptoTxId = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    Fee = table.Column<decimal>(type: "decimal(15,8)", nullable: false),
                    WalletAddress = table.Column<string>(maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUser",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 35, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    AccessHash = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 25, nullable: true),
                    LastName = table.Column<string>(maxLength: 25, nullable: true),
                    UserName = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    ChatId = table.Column<long>(nullable: false),
                    BotId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_TelegramUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TelegramUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mentors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    ChatId = table.Column<long>(nullable: false),
                    BotId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mentors_TelegramUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TelegramUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Fee = table.Column<decimal>(type: "decimal(15,8)", nullable: false),
                    Currency = table.Column<string>(maxLength: 5, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    MentorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscription_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_UserId",
                table: "Mentors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_ClientId",
                table: "Subscription",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_MentorId",
                table: "Subscription",
                column: "MentorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentInfo");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Mentors");

            migrationBuilder.DropTable(
                name: "TelegramUser");
        }
    }
}
