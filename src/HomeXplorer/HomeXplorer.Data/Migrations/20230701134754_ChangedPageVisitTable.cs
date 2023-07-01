using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class ChangedPageVisitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedVisitCookie",
                table: "PageVisits");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Renters",
                type: "int",
                nullable: false,
                comment: "The associated City",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Agents",
                type: "int",
                nullable: false,
                comment: "Reference to the City",
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Renters",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The associated City");

            migrationBuilder.AddColumn<string>(
                name: "HashedVisitCookie",
                table: "PageVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Hashed value of the cookie");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Agents",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Reference to the City");
        }
    }
}
