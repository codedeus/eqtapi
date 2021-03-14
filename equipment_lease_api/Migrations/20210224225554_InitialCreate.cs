using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace equipment_lease_api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetBrands",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetBrands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetBrands_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetBrands_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetCapacities",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCapacities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetCapacities_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetCapacities_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetDimensions",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDimensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetDimensions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetDimensions_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetGroups",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    ParentGroupId = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetGroups_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetGroups_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetGroups_AssetGroups_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalTable: "AssetGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetLeases",
                columns: table => new
                {
                    NativeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LeaseNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SubsidiaryId = table.Column<string>(unicode: false, maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLeases", x => x.NativeId);
                    table.UniqueConstraint("AK_AssetLeases_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetLeases_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetLeases_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetModels",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetModels_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetModels_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetTypes",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetTypes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetTypes_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EngineModels",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineModels_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EngineModels_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EngineTypes",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineTypes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EngineTypes_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaseInvoices",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    InvoiceNumber = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaseInvoices_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locations_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subsidiaries",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsidiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subsidiaries_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subsidiaries_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetItems",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    AssetTypeId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    SerialNumber = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Code = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    RegistrationNumber = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    LeaseCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AssetGroupId = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    AssetSubGroupId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    CurrentStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    AssetId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    AssetBrandId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    AssetModelId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    CapacityId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    DimensionId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    EngineTypeId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    EngineModelId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    EngineNumber = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    AssetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ManufactureYear = table.Column<int>(nullable: true),
                    AcquisitionYear = table.Column<int>(nullable: true),
                    CurrentLocationId = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetItems_AssetBrands_AssetBrandId",
                        column: x => x.AssetBrandId,
                        principalTable: "AssetBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItems_AssetGroups_AssetGroupId",
                        column: x => x.AssetGroupId,
                        principalTable: "AssetGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetItems_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItems_AssetModels_AssetModelId",
                        column: x => x.AssetModelId,
                        principalTable: "AssetModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItems_AssetGroups_AssetSubGroupId",
                        column: x => x.AssetSubGroupId,
                        principalTable: "AssetGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItems_AssetTypes_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalTable: "AssetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItems_AssetCapacities_CapacityId",
                        column: x => x.CapacityId,
                        principalTable: "AssetCapacities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItems_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetItems_Locations_CurrentLocationId",
                        column: x => x.CurrentLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItems_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItems_AssetDimensions_DimensionId",
                        column: x => x.DimensionId,
                        principalTable: "AssetDimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItems_EngineModels_EngineModelId",
                        column: x => x.EngineModelId,
                        principalTable: "EngineModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItems_EngineTypes_EngineTypeId",
                        column: x => x.EngineTypeId,
                        principalTable: "EngineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    LocationId = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    SubsidiaryId = table.Column<string>(unicode: false, maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Subsidiaries_SubsidiaryId",
                        column: x => x.SubsidiaryId,
                        principalTable: "Subsidiaries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetLeaseEntries",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpectedReturnDate = table.Column<DateTime>(nullable: false),
                    ExpectedLeaseOutDate = table.Column<DateTime>(nullable: false),
                    AssetItemId = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    LeaseCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetCurrentStatus = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ActualReturnDate = table.Column<DateTime>(nullable: true),
                    AssetLeaseId = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    ProjectId = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    LocationId = table.Column<string>(unicode: false, maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLeaseEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetLeaseEntries_AssetItems_AssetItemId",
                        column: x => x.AssetItemId,
                        principalTable: "AssetItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetLeaseEntries_AssetLeases_AssetLeaseId",
                        column: x => x.AssetLeaseId,
                        principalTable: "AssetLeases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetLeaseEntries_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetLeaseEntries_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetLeaseEntries_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetLeaseEntries_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectSites",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    ProjectId = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectSites_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSites_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectSites_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetLeaseEntryUpdates",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedById = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AssetStatus = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    InvoiceRaised = table.Column<bool>(nullable: false),
                    LeaseInvoiceId = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Comment = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    AssetLeaseEntryId = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssetBrands_CreatedById",
                table: "AssetBrands",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetBrands_DeletedById",
                table: "AssetBrands",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCapacities_CreatedById",
                table: "AssetCapacities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCapacities_DeletedById",
                table: "AssetCapacities",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDimensions_CreatedById",
                table: "AssetDimensions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDimensions_DeletedById",
                table: "AssetDimensions",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetGroups_CreatedById",
                table: "AssetGroups",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetGroups_DeletedById",
                table: "AssetGroups",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetGroups_ParentGroupId",
                table: "AssetGroups",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_AssetBrandId",
                table: "AssetItems",
                column: "AssetBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_AssetGroupId",
                table: "AssetItems",
                column: "AssetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_AssetId",
                table: "AssetItems",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_AssetModelId",
                table: "AssetItems",
                column: "AssetModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_AssetSubGroupId",
                table: "AssetItems",
                column: "AssetSubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_AssetTypeId",
                table: "AssetItems",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_CapacityId",
                table: "AssetItems",
                column: "CapacityId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_CreatedById",
                table: "AssetItems",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_CurrentLocationId",
                table: "AssetItems",
                column: "CurrentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_DeletedById",
                table: "AssetItems",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_DimensionId",
                table: "AssetItems",
                column: "DimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_EngineModelId",
                table: "AssetItems",
                column: "EngineModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItems_EngineTypeId",
                table: "AssetItems",
                column: "EngineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntries_AssetItemId",
                table: "AssetLeaseEntries",
                column: "AssetItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntries_AssetLeaseId",
                table: "AssetLeaseEntries",
                column: "AssetLeaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntries_CreatedById",
                table: "AssetLeaseEntries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntries_DeletedById",
                table: "AssetLeaseEntries",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntries_LocationId",
                table: "AssetLeaseEntries",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeaseEntries_ProjectId",
                table: "AssetLeaseEntries",
                column: "ProjectId");

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

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeases_CreatedById",
                table: "AssetLeases",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLeases_DeletedById",
                table: "AssetLeases",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetModels_CreatedById",
                table: "AssetModels",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetModels_DeletedById",
                table: "AssetModels",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_CreatedById",
                table: "Assets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_DeletedById",
                table: "Assets",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypes_CreatedById",
                table: "AssetTypes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypes_DeletedById",
                table: "AssetTypes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_EngineModels_CreatedById",
                table: "EngineModels",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EngineModels_DeletedById",
                table: "EngineModels",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_EngineTypes_CreatedById",
                table: "EngineTypes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EngineTypes_DeletedById",
                table: "EngineTypes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseInvoices_CreatedById",
                table: "LeaseInvoices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CreatedById",
                table: "Locations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_DeletedById",
                table: "Locations",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeletedById",
                table: "Projects",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LocationId",
                table: "Projects",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SubsidiaryId",
                table: "Projects",
                column: "SubsidiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSites_CreatedById",
                table: "ProjectSites",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSites_DeletedById",
                table: "ProjectSites",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSites_ProjectId",
                table: "ProjectSites",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiaries_CreatedById",
                table: "Subsidiaries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiaries_DeletedById",
                table: "Subsidiaries",
                column: "DeletedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssetLeaseEntryUpdates");

            migrationBuilder.DropTable(
                name: "ProjectSites");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AssetLeaseEntries");

            migrationBuilder.DropTable(
                name: "LeaseInvoices");

            migrationBuilder.DropTable(
                name: "AssetItems");

            migrationBuilder.DropTable(
                name: "AssetLeases");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "AssetBrands");

            migrationBuilder.DropTable(
                name: "AssetGroups");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "AssetModels");

            migrationBuilder.DropTable(
                name: "AssetTypes");

            migrationBuilder.DropTable(
                name: "AssetCapacities");

            migrationBuilder.DropTable(
                name: "AssetDimensions");

            migrationBuilder.DropTable(
                name: "EngineModels");

            migrationBuilder.DropTable(
                name: "EngineTypes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Subsidiaries");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
