using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kpd37Gomel.DataAccess.Migrations
{
    public partial class ApartmentTenantToTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateUtc",
                table: "Vote",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApartmentId",
                table: "Tenant",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "Tenant",
                nullable: false,
                defaultValue: true);

            migrationBuilder.Sql(
                "UPDATE Tenant SET ApartmentId = AT.ApartmentId, IsOwner = AT.IsOwner FROM Tenant AS T INNER JOIN ApartmentTenant AS AT ON T.Id = AT.TenantId");

            migrationBuilder.DropTable(
                name: "ApartmentTenant");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_ApartmentId",
                table: "Tenant",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_Apartment_ApartmentId",
                table: "Tenant",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_Apartment_ApartmentId",
                table: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_Tenant_ApartmentId",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "DeleteDateUtc",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "Tenant");

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

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentTenant_TenantId",
                table: "ApartmentTenant",
                column: "TenantId");
        }
    }
}
