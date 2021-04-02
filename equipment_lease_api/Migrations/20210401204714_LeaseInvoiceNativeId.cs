using Microsoft.EntityFrameworkCore.Migrations;

namespace equipment_lease_api.Migrations
{
    public partial class LeaseInvoiceNativeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeaseUpdates_LeaseInvoices_LeaseInvoiceId",
                table: "AssetLeaseUpdates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaseInvoices",
                table: "LeaseInvoices");

            migrationBuilder.DropIndex(
                name: "IX_AssetLeaseUpdates_LeaseInvoiceId",
                table: "AssetLeaseUpdates");

            migrationBuilder.DropColumn(
                name: "LeaseInvoiceId",
                table: "AssetLeaseUpdates");

            migrationBuilder.AddColumn<int>(
                name: "NativeId",
                table: "LeaseInvoices",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "LeaseInvoiceNativeId",
                table: "AssetLeaseUpdates",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_LeaseInvoices_Id",
                table: "LeaseInvoices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaseInvoices",
                table: "LeaseInvoices",
                column: "NativeId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeaseUpdates_LeaseInvoices_LeaseInvoiceNativeId",
                table: "AssetLeaseUpdates");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_LeaseInvoices_Id",
                table: "LeaseInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaseInvoices",
                table: "LeaseInvoices");

            migrationBuilder.DropIndex(
                name: "IX_AssetLeaseUpdates_LeaseInvoiceNativeId",
                table: "AssetLeaseUpdates");

            migrationBuilder.DropColumn(
                name: "NativeId",
                table: "LeaseInvoices");

            migrationBuilder.DropColumn(
                name: "LeaseInvoiceNativeId",
                table: "AssetLeaseUpdates");

            migrationBuilder.AddColumn<string>(
                name: "LeaseInvoiceId",
                table: "AssetLeaseUpdates",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaseInvoices",
                table: "LeaseInvoices",
                column: "Id");

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
    }
}
