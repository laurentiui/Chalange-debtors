using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations.MsSql.Migrations
{
    public partial class ch3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Debtors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Debtors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
