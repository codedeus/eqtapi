using Microsoft.EntityFrameworkCore.Migrations;

namespace equipment_lease_api.Migrations
{
    public partial class AssetLeaseLeaseInvoiceLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaseInvoices_AssetLeases_AssetLeaseNativeId",
                table: "LeaseInvoices");

            migrationBuilder.DropIndex(
                name: "IX_LeaseInvoices_AssetLeaseNativeId",
                table: "LeaseInvoices");

            migrationBuilder.DropColumn(
                name: "AssetLeaseNativeId",
                table: "LeaseInvoices");

            migrationBuilder.AlterColumn<string>(
                name: "AssetLeaseId",
                table: "LeaseInvoices",
                unicode: false,
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaseInvoices_AssetLeaseId",
                table: "LeaseInvoices",
                column: "AssetLeaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseInvoices_AssetLeases_AssetLeaseId",
                table: "LeaseInvoices",
                column: "AssetLeaseId",
                principalTable: "AssetLeases",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaseInvoices_AssetLeases_AssetLeaseId",
                table: "LeaseInvoices");

            migrationBuilder.DropIndex(
                name: "IX_LeaseInvoices_AssetLeaseId",
                table: "LeaseInvoices");

            migrationBuilder.AlterColumn<string>(
                name: "AssetLeaseId",
                table: "LeaseInvoices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 256);

            migrationBuilder.AddColumn<int>(
                name: "AssetLeaseNativeId",
                table: "LeaseInvoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaseInvoices_AssetLeaseNativeId",
                table: "LeaseInvoices",
                column: "AssetLeaseNativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseInvoices_AssetLeases_AssetLeaseNativeId",
                table: "LeaseInvoices",
                column: "AssetLeaseNativeId",
                principalTable: "AssetLeases",
                principalColumn: "NativeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
