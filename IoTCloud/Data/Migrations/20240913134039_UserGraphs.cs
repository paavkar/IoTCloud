using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoTCloud.Migrations
{
    /// <inheritdoc />
    public partial class UserGraphs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GraphItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraphType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGraphItems",
                columns: table => new
                {
                    GraphId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGraphItems",
                        columns: gi => new { gi.GraphId, gi.UserId });
                    table.ForeignKey(
                        name: "FK_UserGraphItems_GraphItems_Id",
                        column: x => x.GraphId,
                        principalTable: "GraphItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGraphItems_AspNetUsers_Id",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                }
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGraphItems_GraphItems_Id",
                table: "GraphItems");
            migrationBuilder.DropForeignKey(
                name: "FK_UserGraphItems_AspNetUsers_Id",
                table: "GraphItems");
            migrationBuilder.DropTable(
                name: "GraphItems");
            migrationBuilder.DropTable(
                name: "UserGraphItems");
        }
    }
}
