using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoTCloud.Migrations
{
    /// <inheritdoc />
    public partial class TablesReadingsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SensorName",
                table: "VelocityReadings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SensorName",
                table: "TemperatureReadings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SensorName",
                table: "LuminosityReadings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SensorName",
                table: "HumidityReadings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SensorName",
                table: "GraphItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserGraphItems_GraphItems_Id",
            //    table: "UserGraphItems");
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserGraphItems_AspNetUsers_Id",
            //    table: "UserGraphItems");

            //migrationBuilder.DropTable(
            //    name: "UserGraphItems");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GraphItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_GraphItems_AspNetUsers_UserId",
                table: "GraphItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddColumn<string>(
                name: "SensorName",
                table: "EmailNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SensorName",
                table: "DistanceReadings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SensorName",
                table: "BinaryReadings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TableItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReadingType = table.Column<int>(type: "int", nullable: false),
                    IsBinary = table.Column<bool>(type: "bit", nullable: false),
                    SensorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableItems");

            migrationBuilder.DropColumn(
                name: "SensorName",
                table: "VelocityReadings");

            migrationBuilder.DropColumn(
                name: "SensorName",
                table: "TemperatureReadings");

            migrationBuilder.DropColumn(
                name: "SensorName",
                table: "LuminosityReadings");

            migrationBuilder.DropColumn(
                name: "SensorName",
                table: "HumidityReadings");

            migrationBuilder.DropColumn(
                name: "SensorName",
                table: "GraphItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GraphItems_AspNetUsers_UserId",
                table: "GraphItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GraphItems");

            migrationBuilder.DropColumn(
                name: "SensorName",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "SensorName",
                table: "DistanceReadings");

            migrationBuilder.DropColumn(
                name: "SensorName",
                table: "BinaryReadings");
        }
    }
}
