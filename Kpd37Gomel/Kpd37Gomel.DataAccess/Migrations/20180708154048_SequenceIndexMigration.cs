using Microsoft.EntityFrameworkCore.Migrations;

namespace Kpd37Gomel.DataAccess.Migrations
{
    public partial class SequenceIndexMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SequenceIndex",
                table: "VoteVariant",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SequenceIndex",
                table: "VoteVariant");
        }
    }
}
