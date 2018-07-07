using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kpd37Gomel.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApartmentNumber = table.Column<int>(nullable: false),
                    FloorNumber = table.Column<int>(nullable: true),
                    TotalAreaSnb = table.Column<double>(nullable: true),
                    TotalArea = table.Column<double>(nullable: false),
                    LivingSpace = table.Column<double>(nullable: true),
                    VoteRate = table.Column<double>(nullable: false),
                    RoomsCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentTenant",
                columns: table => new
                {
                    ApartmentId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    IsOwner = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentTenant", x => new { x.ApartmentId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_ApartmentTenant_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApartmentTenant_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    CreateDateUtc = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    UseVoteRate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vote_Tenant_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoteVariant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VoteId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteVariant_Vote_VoteId",
                        column: x => x.VoteId,
                        principalTable: "Vote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoteChoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VoteId = table.Column<Guid>(nullable: false),
                    VoteVariantId = table.Column<Guid>(nullable: false),
                    ApartmentId = table.Column<Guid>(nullable: false),
                    VoteRate = table.Column<double>(nullable: true),
                    VoteDateUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteChoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteChoice_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteChoice_Vote_VoteId",
                        column: x => x.VoteId,
                        principalTable: "Vote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteChoice_VoteVariant_VoteVariantId",
                        column: x => x.VoteVariantId,
                        principalTable: "VoteVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentTenant_TenantId",
                table: "ApartmentTenant",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_AuthorId",
                table: "Vote",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteChoice_ApartmentId",
                table: "VoteChoice",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteChoice_VoteId",
                table: "VoteChoice",
                column: "VoteId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteChoice_VoteVariantId",
                table: "VoteChoice",
                column: "VoteVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteVariant_VoteId",
                table: "VoteVariant",
                column: "VoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentTenant");

            migrationBuilder.DropTable(
                name: "VoteChoice");

            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "VoteVariant");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
