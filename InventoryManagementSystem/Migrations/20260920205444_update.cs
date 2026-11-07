using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Products_ProductId",
                table: "StockTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTransaction",
                table: "StockTransaction");

            migrationBuilder.RenameTable(
                name: "StockTransaction",
                newName: "StockTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransaction_ProductId",
                table: "StockTransactions",
                newName: "IX_StockTransactions_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTransactions",
                table: "StockTransactions",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Products_ProductId",
                table: "StockTransactions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Products_ProductId",
                table: "StockTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTransactions",
                table: "StockTransactions");

            migrationBuilder.RenameTable(
                name: "StockTransactions",
                newName: "StockTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransactions_ProductId",
                table: "StockTransaction",
                newName: "IX_StockTransaction_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTransaction",
                table: "StockTransaction",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_Products_ProductId",
                table: "StockTransaction",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
