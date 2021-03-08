using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioManagementSystem.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockDetails_StockId",
                table: "StockDetails");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_StockId",
                table: "StockDetails",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockDetails_StockId",
                table: "StockDetails");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_StockId",
                table: "StockDetails",
                column: "StockId",
                unique: true);
        }
    }
}
