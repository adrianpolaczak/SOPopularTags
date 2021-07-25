using Microsoft.EntityFrameworkCore.Migrations;

namespace SOPopularTags.Infrastructure.Migrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasMore",
                table: "SOTagRequests");

            migrationBuilder.DropColumn(
                name: "QuotaMax",
                table: "SOTagRequests");

            migrationBuilder.DropColumn(
                name: "QuotaRemaining",
                table: "SOTagRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasMore",
                table: "SOTagRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QuotaMax",
                table: "SOTagRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotaRemaining",
                table: "SOTagRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
