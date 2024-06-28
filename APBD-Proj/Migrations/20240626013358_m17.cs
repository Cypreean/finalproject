using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Proj.Migrations
{
    /// <inheritdoc />
    public partial class m17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionsPayments",
                columns: table => new
                {
                    IdPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSubscription = table.Column<int>(type: "int", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionsPayments", x => x.IdPayment);
                    table.ForeignKey(
                        name: "FK_SubscriptionsPayments_Subscriptions_IdSubscription",
                        column: x => x.IdSubscription,
                        principalTable: "Subscriptions",
                        principalColumn: "SubscriptionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionsPayments_IdSubscription",
                table: "SubscriptionsPayments",
                column: "IdSubscription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionsPayments");
        }
    }
}
