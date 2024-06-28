using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Proj.Migrations
{
    /// <inheritdoc />
    public partial class m6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Discounts_DiscountsIdDiscount",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_DiscountsIdDiscount",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DiscountsIdDiscount",
                table: "Contracts");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DiscountID",
                table: "Contracts",
                column: "DiscountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Discounts_DiscountID",
                table: "Contracts",
                column: "DiscountID",
                principalTable: "Discounts",
                principalColumn: "DiscountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Discounts_DiscountID",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_DiscountID",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "DiscountsIdDiscount",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DiscountsIdDiscount",
                table: "Contracts",
                column: "DiscountsIdDiscount");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Discounts_DiscountsIdDiscount",
                table: "Contracts",
                column: "DiscountsIdDiscount",
                principalTable: "Discounts",
                principalColumn: "DiscountID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
