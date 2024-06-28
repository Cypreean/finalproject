using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Proj.Migrations
{
    /// <inheritdoc />
    public partial class m8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Versions_VersionIdVersion",
                table: "Softwares");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropIndex(
                name: "IX_Softwares_VersionIdVersion",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "VersionIdVersion",
                table: "Softwares");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VersionIdVersion",
                table: "Softwares",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    IdVersion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.IdVersion);
                });

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
    }
}
