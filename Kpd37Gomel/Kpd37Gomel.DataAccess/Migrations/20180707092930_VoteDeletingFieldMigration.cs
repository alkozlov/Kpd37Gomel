using Microsoft.EntityFrameworkCore.Migrations;

namespace Kpd37Gomel.DataAccess.Migrations
{
    public partial class VoteDeletingFieldMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vote",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vote");
        }
    }
}
