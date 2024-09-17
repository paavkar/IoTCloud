using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoTCloud.Migrations
{
    /// <inheritdoc />
    public partial class BinaryGraphs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BinaryGraphItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReadingType = table.Column<int>(type: "int", nullable: false),
                    SensorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinaryGraphItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinaryGraphItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinaryGraphItems_AspNetUsers_UserId",
                table: "BinaryGraphItems");

            migrationBuilder.DropTable(
                name: "BinaryGraphItems");
        }
    }
}
