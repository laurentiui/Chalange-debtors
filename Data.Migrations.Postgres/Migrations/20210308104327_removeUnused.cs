using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations.Postgres.Migrations
{
    public partial class removeUnused : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsAllowed", "Username" },
                values: new object[] { false, "brown-candies" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsAllowed", "Username" },
                values: new object[] { true, "userJustForTest-to-ensure-you-check-the-db" });
        }
    }
}
