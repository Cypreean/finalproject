using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Proj.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Version",
                table: "Softwares",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Version",
                columns: table => new
                {
                    IdVersion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version", x => x.IdVersion);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_Version",
                table: "Softwares",
                column: "Version");

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Version_Version",
                table: "Softwares",
                column: "Version",
                principalTable: "Version",
                principalColumn: "IdVersion",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Version_Version",
                table: "Softwares");

            migrationBuilder.DropTable(
                name: "Version");

            migrationBuilder.DropIndex(
                name: "IX_Softwares_Version",
                table: "Softwares");

            migrationBuilder.AlterColumn<string>(
                name: "Version",
                table: "Softwares",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
