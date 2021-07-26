using Microsoft.EntityFrameworkCore.Migrations;

namespace SOPopularTags.Infrastructure.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SOTagRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOTagRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SOTagRequestItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SOTagRequestId = table.Column<int>(type: "int", nullable: false),
                    HasSynonyms = table.Column<bool>(type: "bit", nullable: false),
                    IsModeratorOnly = table.Column<bool>(type: "bit", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularityPercent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOTagRequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SOTagRequestItems_SOTagRequests_SOTagRequestId",
                        column: x => x.SOTagRequestId,
                        principalTable: "SOTagRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SOTagRequestItems_SOTagRequestId",
                table: "SOTagRequestItems",
                column: "SOTagRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SOTagRequestItems");

            migrationBuilder.DropTable(
                name: "SOTagRequests");
        }
    }
}
