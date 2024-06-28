using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Proj.Migrations
{
    /// <inheritdoc />
    public partial class m7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Versions_IdVersion",
                table: "Softwares");

            migrationBuilder.DropIndex(
                name: "IX_Softwares_IdVersion",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "IdVersion",
                table: "Softwares");

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Softwares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VersionIdVersion",
                table: "Softwares",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_VersionIdVersion",
                table: "Softwares",
                column: "VersionIdVersion");

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Versions_VersionIdVersion",
                table: "Softwares",
                column: "VersionIdVersion",
                principalTable: "Versions",
                principalColumn: "IdVersion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Versions_VersionIdVersion",
                table: "Softwares");

            migrationBuilder.DropIndex(
                name: "IX_Softwares_VersionIdVersion",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "VersionIdVersion",
                table: "Softwares");

            migrationBuilder.AddColumn<int>(
                name: "IdVersion",
                table: "Softwares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_IdVersion",
                table: "Softwares",
                column: "IdVersion");

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Versions_IdVersion",
                table: "Softwares",
                column: "IdVersion",
                principalTable: "Versions",
                principalColumn: "IdVersion",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
