using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kpd37Gomel.DataAccess.Migrations
{
    public partial class VoteChoisesToDeepLevelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleteDateUtc",
                table: "Vote",
                newName: "DeletionDateUtc");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDateUtc",
                table: "Tenant",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tenant",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDateUtc",
                table: "Apartment",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Apartment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ApartmentVoteChoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VoteVariantId = table.Column<Guid>(nullable: false),
                    ApartmentId = table.Column<Guid>(nullable: false),
                    VoteRate = table.Column<double>(nullable: true),
                    ParticipationDateUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentVoteChoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApartmentVoteChoice_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApartmentVoteChoice_VoteVariant_VoteVariantId",
                        column: x => x.VoteVariantId,
                        principalTable: "VoteVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentVoteChoice_ApartmentId",
                table: "ApartmentVoteChoice",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentVoteChoice_VoteVariantId",
                table: "ApartmentVoteChoice",
                column: "VoteVariantId");

            migrationBuilder.Sql(
                "INSERT INTO ApartmentVoteChoice (Id,VoteVariantId,ApartmentId,VoteRate,ParticipationDateUtc) SELECT NEWID(),VoteVariantId,ApartmentId,VoteRate,VoteDateUtc FROM VoteChoice");

            migrationBuilder.DropTable(
                name: "VoteChoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentVoteChoice");

            migrationBuilder.DropColumn(
                name: "DeletionDateUtc",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "DeletionDateUtc",
                table: "Apartment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Apartment");

            migrationBuilder.RenameColumn(
                name: "DeletionDateUtc",
                table: "Vote",
                newName: "DeleteDateUtc");

            migrationBuilder.CreateTable(
                name: "VoteChoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApartmentId = table.Column<Guid>(nullable: false),
                    VoteDateUtc = table.Column<DateTime>(nullable: false),
                    VoteId = table.Column<Guid>(nullable: false),
                    VoteRate = table.Column<double>(nullable: true),
                    VoteVariantId = table.Column<Guid>(nullable: false)
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
        }
    }
}
