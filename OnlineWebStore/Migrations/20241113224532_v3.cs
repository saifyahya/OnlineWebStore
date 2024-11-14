using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineWebStore.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_stores_StoreId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_StoreId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "StoreId1",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_StoreId1",
                table: "products",
                column: "StoreId1");

            migrationBuilder.AddForeignKey(
                name: "FK_products_stores_StoreId1",
                table: "products",
                column: "StoreId1",
                principalTable: "stores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_stores_StoreId1",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_StoreId1",
                table: "products");

            migrationBuilder.DropColumn(
                name: "StoreId1",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_StoreId",
                table: "products",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_stores_StoreId",
                table: "products",
                column: "StoreId",
                principalTable: "stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
