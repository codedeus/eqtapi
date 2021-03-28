using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace equipment_lease_api.Migrations
{
    public partial class AssetLeaseUpdateConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeaseEntries_AssetItems_AssetItemId",
                table: "AssetLeaseEntries");

            migrationBuilder.DropTable(
                name: "AssetLeaseEntryUpdates");

            migrationBuilder.DropColumn(
                name: "LeaseId",
                table: "LeaseInvoices");

            migrationBuilder.AddColumn<string>(
                name: "AssetLeaseId",
                table: "LeaseInvoices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssetLeaseNativeId",
                table: "LeaseInvoices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "LeaseInvoices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "AssetLeaseEntries",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssetLeaseUpdates",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    InvoiceRaised = table.Column<bool>(nullable: false),
                    AssetLeaseId = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    LeaseInvoiceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLeaseUpdates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetLeaseUpdates_AssetLeases_AssetLeaseId",
                        column: x => x.AssetLeaseId,
                        principalTable: "AssetLeases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetLeaseUpdates_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetLeaseUpdates_LeaseInvoices_LeaseInvoiceId",
                        column: x => x.LeaseInvoiceId,
                        principalTable: "LeaseInvoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetLeaseUpdateEntries",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AssetStatus = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    AssetLeaseEntryId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    AssetLeaseUpdateId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLeaseUpdateEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetLeaseUpdateEntries_AssetLeaseEntries_AssetLeaseEntryId",
                        column: x => x.AssetLeaseEntryId,
                        principalTable: "AssetLeaseEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetLeaseUpdateEntries_AssetLeaseUpdates_AssetLeaseUpdateId",
                        column: x => x.AssetLeaseUpdateId,
                        principalTable: "AssetLeaseUpdates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetLeaseUpdateEntries_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetLeaseUpdateEntries_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaseInvoices_AssetLeaseNativeId",
                table: "LeaseInvoices",
                column: "AssetLeaseNativeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseUpdateEntries_AssetLeaseEntryId",
                table: "AssetLeaseUpdateEntries",
                column: "AssetLeaseEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseUpdateEntries_AssetLeaseUpdateId",
                table: "AssetLeaseUpdateEntries",
                column: "AssetLeaseUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseUpdateEntries_CreatedById",
                table: "AssetLeaseUpdateEntries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseUpdateEntries_DeletedById",
                table: "AssetLeaseUpdateEntries",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseUpdates_AssetLeaseId",
                table: "AssetLeaseUpdates",
                column: "AssetLeaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseUpdates_CreatedById",
                table: "AssetLeaseUpdates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseUpdates_LeaseInvoiceId",
                table: "AssetLeaseUpdates",
                column: "LeaseInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLeaseEntries_AssetItems_AssetItemId",
                table: "AssetLeaseEntries",
                column: "AssetItemId",
                principalTable: "AssetItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseInvoices_AssetLeases_AssetLeaseNativeId",
                table: "LeaseInvoices",
                column: "AssetLeaseNativeId",
                principalTable: "AssetLeases",
                principalColumn: "NativeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLeaseEntries_AssetItems_AssetItemId",
                table: "AssetLeaseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaseInvoices_AssetLeases_AssetLeaseNativeId",
                table: "LeaseInvoices");

            migrationBuilder.DropTable(
                name: "AssetLeaseUpdateEntries");

            migrationBuilder.DropTable(
                name: "AssetLeaseUpdates");

            migrationBuilder.DropIndex(
                name: "IX_LeaseInvoices_AssetLeaseNativeId",
                table: "LeaseInvoices");

            migrationBuilder.DropColumn(
                name: "AssetLeaseId",
                table: "LeaseInvoices");

            migrationBuilder.DropColumn(
                name: "AssetLeaseNativeId",
                table: "LeaseInvoices");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "LeaseInvoices");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "AssetLeaseEntries");

            migrationBuilder.AddColumn<string>(
                name: "LeaseId",
                table: "LeaseInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssetLeaseEntryUpdates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    AssetLeaseEntryId = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    AssetStatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Comment = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    CreatedById = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedById = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    InvoiceRaised = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LeaseInvoiceId = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLeaseEntryUpdates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetLeaseEntryUpdates_AssetLeaseEntries_AssetLeaseEntryId",
                        column: x => x.AssetLeaseEntryId,
                        principalTable: "AssetLeaseEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetLeaseEntryUpdates_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetLeaseEntryUpdates_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetLeaseEntryUpdates_LeaseInvoices_LeaseInvoiceId",
                        column: x => x.LeaseInvoiceId,
                        principalTable: "LeaseInvoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntryUpdates_AssetLeaseEntryId",
                table: "AssetLeaseEntryUpdates",
                column: "AssetLeaseEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntryUpdates_CreatedById",
                table: "AssetLeaseEntryUpdates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntryUpdates_DeletedById",
                table: "AssetLeaseEntryUpdates",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntryUpdates_LeaseInvoiceId",
                table: "AssetLeaseEntryUpdates",
                column: "LeaseInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLeaseEntries_AssetItems_AssetItemId",
                table: "AssetLeaseEntries",
                column: "AssetItemId",
                principalTable: "AssetItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
