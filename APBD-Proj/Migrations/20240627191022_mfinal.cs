using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APBD_Proj.Migrations
{
    /// <inheritdoc />
    public partial class mfinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyID", "Address", "Email", "KRS", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Tech Street", "contact@techcorp.com", 12345678901234L, "TechCorp", 555123456 },
                    { 2, "456 Biz Avenue", "info@bizcorp.com", 43210987654321L, "BizCorp", 555654321 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Email", "FirstName", "IsDeleted", "LastName", "Pesel", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", false, "Doe", 12345678901L, 123456789 },
                    { 2, "jane.smith@example.com", "Jane", false, "Smith", 98765432101L, 987654321 }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "DiscountID", "EndDate", "Name", "Percentage", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 17, 21, 10, 22, 54, DateTimeKind.Local).AddTicks(8795), "Summer Sale", 10.0m, new DateTime(2024, 6, 17, 21, 10, 22, 52, DateTimeKind.Local).AddTicks(4970) },
                    { 2, new DateTime(2024, 6, 17, 21, 10, 22, 54, DateTimeKind.Local).AddTicks(9103), "Winter Sale", 20.0m, new DateTime(2024, 5, 28, 21, 10, 22, 54, DateTimeKind.Local).AddTicks(9093) }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "hashedpassword", "Administrator", "admin" },
                    { 2, "hashedpassword", "User", "user" }
                });

            migrationBuilder.InsertData(
                table: "Softwares",
                columns: new[] { "SoftwareID", "Category", "Description", "Name", "Version" },
                values: new object[,]
                {
                    { 1, "Utility", "A super application", "SuperApp", "1.0" },
                    { 2, "Business", "A mega application", "MegaApp", "2.0" }
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "ContractID", "EndDate", "CompanyID", "CustomerID", "DiscountID", "SoftwareID", "IsPaid", "IsSigned", "StartDate", "TotalAmount", "YearsOfSupport" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 27, 21, 10, 22, 55, DateTimeKind.Local).AddTicks(2049), 1, 1, 1, 1, true, true, new DateTime(2024, 6, 27, 21, 10, 22, 55, DateTimeKind.Local).AddTicks(1850), 1000.0m, 1 },
                    { 2, new DateTime(2025, 6, 27, 21, 10, 22, 55, DateTimeKind.Local).AddTicks(3155), 2, 2, 2, 2, false, false, new DateTime(2024, 6, 27, 21, 10, 22, 55, DateTimeKind.Local).AddTicks(3146), 2000.0m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "SubscriptionID", "EndDate", "CompanyID", "CustomerID", "SoftwareID", "IsActive", "Price", "RenewalPeriod", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 27, 21, 10, 22, 55, DateTimeKind.Local).AddTicks(6316), 1, 1, 1, true, 100.0m, 12, new DateTime(2024, 6, 27, 21, 10, 22, 55, DateTimeKind.Local).AddTicks(6127) },
                    { 2, new DateTime(2026, 6, 27, 21, 10, 22, 55, DateTimeKind.Local).AddTicks(6880), 2, 2, 2, false, 200.0m, 24, new DateTime(2024, 6, 27, 21, 10, 22, 55, DateTimeKind.Local).AddTicks(6871) }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentID", "Amount", "ClientInfo", "ContractID", "PaymentDate" },
                values: new object[,]
                {
                    { 1, 500.0m, "John Doe", 1, new DateTime(2024, 6, 27, 21, 10, 22, 55, DateTimeKind.Local).AddTicks(4258) },
                    { 2, 1000.0m, "Jane Smith", 2, new DateTime(2024, 6, 27, 21, 10, 22, 55, DateTimeKind.Local).AddTicks(4752) }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionsPayments",
                columns: new[] { "IdPayment", "Ammount", "IdSubscription" },
                values: new object[,]
                {
                    { 1, 100.0m, 1 },
                    { 2, 200.0m, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubscriptionsPayments",
                keyColumn: "IdPayment",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubscriptionsPayments",
                keyColumn: "IdPayment",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "ContractID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "ContractID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubscriptionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubscriptionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "DiscountID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "DiscountID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "SoftwareID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "SoftwareID",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "IdSubscription",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdSubscription",
                table: "Payments",
                column: "IdSubscription");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Subscriptions_IdSubscription",
                table: "Payments",
                column: "IdSubscription",
                principalTable: "Subscriptions",
                principalColumn: "SubscriptionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
