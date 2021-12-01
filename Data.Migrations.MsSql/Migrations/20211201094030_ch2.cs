using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations.MsSql.Migrations
{
    public partial class ch2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Debtors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Debtors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Debtors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Debtors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Debtors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Debtors");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Debtors");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Debtors");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Debtors");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Debtors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
