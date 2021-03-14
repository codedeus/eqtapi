using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace equipment_lease_api.Migrations
{
    public partial class SomeRelationshipCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeaseEntries_Locations_LocationId",
                table: "AssetLeaseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeaseEntries_Projects_ProjectId",
                table: "AssetLeaseEntries");

            migrationBuilder.DropIndex(
                name: "IX_AssetLeaseEntries_LocationId",
                table: "AssetLeaseEntries");

            migrationBuilder.DropIndex(
                name: "IX_AssetLeaseEntries_ProjectId",
                table: "AssetLeaseEntries");

            migrationBuilder.DropColumn(
                name: "SubsidiaryId",
                table: "AssetLeases");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "AssetLeaseEntries");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "AssetLeaseEntries");

            migrationBuilder.AddColumn<DateTime>(
                name: "LeaseEndDate",
                table: "LeaseInvoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LeaseId",
                table: "LeaseInvoices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LeaseStartDate",
                table: "LeaseInvoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "AssetLeases",
                unicode: false,
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectSiteId",
                table: "AssetLeaseEntries",
                unicode: false,
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeases_ProjectId",
                table: "AssetLeases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntries_ProjectSiteId",
                table: "AssetLeaseEntries",
                column: "ProjectSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLeaseEntries_ProjectSites_ProjectSiteId",
                table: "AssetLeaseEntries",
                column: "ProjectSiteId",
                principalTable: "ProjectSites",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLeases_Projects_ProjectId",
                table: "AssetLeases",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeaseEntries_ProjectSites_ProjectSiteId",
                table: "AssetLeaseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeases_Projects_ProjectId",
                table: "AssetLeases");

            migrationBuilder.DropIndex(
                name: "IX_AssetLeases_ProjectId",
                table: "AssetLeases");

            migrationBuilder.DropIndex(
                name: "IX_AssetLeaseEntries_ProjectSiteId",
                table: "AssetLeaseEntries");

            migrationBuilder.DropColumn(
                name: "LeaseEndDate",
                table: "LeaseInvoices");

            migrationBuilder.DropColumn(
                name: "LeaseId",
                table: "LeaseInvoices");

            migrationBuilder.DropColumn(
                name: "LeaseStartDate",
                table: "LeaseInvoices");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "AssetLeases");

            migrationBuilder.DropColumn(
                name: "ProjectSiteId",
                table: "AssetLeaseEntries");

            migrationBuilder.AddColumn<string>(
                name: "SubsidiaryId",
                table: "AssetLeases",
                type: "varchar(256)",
                unicode: false,
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "AssetLeaseEntries",
                type: "varchar(256)",
                unicode: false,
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "AssetLeaseEntries",
                type: "varchar(256)",
                unicode: false,
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntries_LocationId",
                table: "AssetLeaseEntries",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntries_ProjectId",
                table: "AssetLeaseEntries",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLeaseEntries_Locations_LocationId",
                table: "AssetLeaseEntries",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLeaseEntries_Projects_ProjectId",
                table: "AssetLeaseEntries",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
