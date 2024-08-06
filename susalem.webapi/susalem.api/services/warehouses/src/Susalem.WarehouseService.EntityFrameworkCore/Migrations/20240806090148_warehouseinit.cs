using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Susalem.WarehouseService.Migrations
{
    public partial class warehouseinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
