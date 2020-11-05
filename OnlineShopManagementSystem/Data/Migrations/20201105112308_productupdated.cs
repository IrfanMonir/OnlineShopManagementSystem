using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopManagementSystem.Data.Migrations
{
    public partial class productupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypesId",
                table: "ProductTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagId",
                table: "Tags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ProductTypesId",
                table: "ProductTypes",
                column: "ProductTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_ProductTypes_ProductTypesId",
                table: "ProductTypes",
                column: "ProductTypesId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Tags_TagId",
                table: "Tags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_ProductTypes_ProductTypesId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Tags_TagId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TagId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_ProductTypesId",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ProductTypesId",
                table: "ProductTypes");
        }
    }
}
