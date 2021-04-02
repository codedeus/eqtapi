using Microsoft.EntityFrameworkCore.Migrations;

namespace equipment_lease_api.Migrations
{
    public partial class LeaseUpdateInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeaseUpdates_LeaseInvoices_LeaseInvoiceNativeId",
                table: "AssetLeaseUpdates");

            migrationBuilder.DropIndex(
                name: "IX_AssetLeaseUpdates_LeaseInvoiceNativeId",
                table: "AssetLeaseUpdates");

            migrationBuilder.DropColumn(
                name: "LeaseInvoiceNativeId",
                table: "AssetLeaseUpdates");

            migrationBuilder.AddColumn<string>(
                name: "LeaseInvoiceId",
                table: "AssetLeaseUpdates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseUpdates_LeaseInvoiceId",
                table: "AssetLeaseUpdates",
                column: "LeaseInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLeaseUpdates_LeaseInvoices_LeaseInvoiceId",
                table: "AssetLeaseUpdates",
                column: "LeaseInvoiceId",
                principalTable: "LeaseInvoices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeaseUpdates_LeaseInvoices_LeaseInvoiceId",
                table: "AssetLeaseUpdates");

            migrationBuilder.DropIndex(
                name: "IX_AssetLeaseUpdates_LeaseInvoiceId",
                table: "AssetLeaseUpdates");

            migrationBuilder.DropColumn(
                name: "LeaseInvoiceId",
                table: "AssetLeaseUpdates");

            migrationBuilder.AddColumn<int>(
                name: "LeaseInvoiceNativeId",
                table: "AssetLeaseUpdates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseUpdates_LeaseInvoiceNativeId",
                table: "AssetLeaseUpdates",
                column: "LeaseInvoiceNativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLeaseUpdates_LeaseInvoices_LeaseInvoiceNativeId",
                table: "AssetLeaseUpdates",
                column: "LeaseInvoiceNativeId",
                principalTable: "LeaseInvoices",
                principalColumn: "NativeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
