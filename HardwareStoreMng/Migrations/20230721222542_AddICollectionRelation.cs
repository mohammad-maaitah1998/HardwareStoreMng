using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HardwareStoreMng.Migrations
{
    public partial class AddICollectionRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceItem_ProductId",
                table: "InvoiceItem");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_ProductId",
                table: "InvoiceItem",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceItem_ProductId",
                table: "InvoiceItem");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_ProductId",
                table: "InvoiceItem",
                column: "ProductId",
                unique: true);
        }
    }
}
