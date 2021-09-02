using Microsoft.EntityFrameworkCore.Migrations;

namespace Test1.Migrations
{
    public partial class tablePBB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseByUsers",
                columns: table => new
                {
                    PurchaseByUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvoucherId = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    ReceiverPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseByUsers", x => x.PurchaseByUserId);
                    table.ForeignKey(
                        name: "FK_PurchaseByUsers_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "BuyerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseByUsers_EVouchers_EvoucherId",
                        column: x => x.EvoucherId,
                        principalTable: "EVouchers",
                        principalColumn: "EvoucherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseByUsers_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseByUsers_BuyerId",
                table: "PurchaseByUsers",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseByUsers_EvoucherId",
                table: "PurchaseByUsers",
                column: "EvoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseByUsers_PaymentId",
                table: "PurchaseByUsers",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseByUsers");
        }
    }
}
