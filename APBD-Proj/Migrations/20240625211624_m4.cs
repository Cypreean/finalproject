using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Proj.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Version_Version",
                table: "Softwares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Version",
                table: "Version");

            migrationBuilder.RenameTable(
                name: "Version",
                newName: "Versions");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Softwares",
                newName: "IdVersion");

            migrationBuilder.RenameIndex(
                name: "IX_Softwares_Version",
                table: "Softwares",
                newName: "IX_Softwares_IdVersion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Versions",
                table: "Versions",
                column: "IdVersion");

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Versions_IdVersion",
                table: "Softwares",
                column: "IdVersion",
                principalTable: "Versions",
                principalColumn: "IdVersion",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Versions_IdVersion",
                table: "Softwares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Versions",
                table: "Versions");

            migrationBuilder.RenameTable(
                name: "Versions",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "IdVersion",
                table: "Softwares",
                newName: "Version");

            migrationBuilder.RenameIndex(
                name: "IX_Softwares_IdVersion",
                table: "Softwares",
                newName: "IX_Softwares_Version");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Version",
                table: "Version",
                column: "IdVersion");

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Version_Version",
                table: "Softwares",
                column: "Version",
                principalTable: "Version",
                principalColumn: "IdVersion",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
