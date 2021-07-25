using Microsoft.EntityFrameworkCore.Migrations;

namespace SOPopularTags.Infrastructure.Migrations
{
    public partial class UpdateTables5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PopularityPercent",
                table: "SOTagRequestItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PopularityPercent",
                table: "SOTagRequestItems");
        }
    }
}
