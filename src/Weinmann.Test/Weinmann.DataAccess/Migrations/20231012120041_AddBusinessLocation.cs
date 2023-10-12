using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weinmann.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessLocationId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BusinessLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessLocations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BusinessLocationId",
                table: "Customers",
                column: "BusinessLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_BusinessLocations_BusinessLocationId",
                table: "Customers",
                column: "BusinessLocationId",
                principalTable: "BusinessLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_BusinessLocations_BusinessLocationId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "BusinessLocations");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BusinessLocationId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BusinessLocationId",
                table: "Customers");
        }
    }
}
