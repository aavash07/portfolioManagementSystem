using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioManagementSystem.Migrations
{
    public partial class revert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_StockId",
                table: "StockDetails",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockDetails_Stocks_StockId",
                table: "StockDetails",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockDetails_Stocks_StockId",
                table: "StockDetails");

            migrationBuilder.DropIndex(
                name: "IX_StockDetails_StockId",
                table: "StockDetails");
        }
    }
}
