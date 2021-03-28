using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace equipment_lease_api.Migrations
{
    public partial class LeaseUpdateEntryLogModificationDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "AssetLeaseUpdateEntries",
                unicode: false,
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "AssetLeaseUpdateEntries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseUpdateEntries_LastModifiedById",
                table: "AssetLeaseUpdateEntries",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLeaseUpdateEntries_AspNetUsers_LastModifiedById",
                table: "AssetLeaseUpdateEntries",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeaseUpdateEntries_AspNetUsers_LastModifiedById",
                table: "AssetLeaseUpdateEntries");

            migrationBuilder.DropIndex(
                name: "IX_AssetLeaseUpdateEntries_LastModifiedById",
                table: "AssetLeaseUpdateEntries");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "AssetLeaseUpdateEntries");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "AssetLeaseUpdateEntries");
        }
    }
}
