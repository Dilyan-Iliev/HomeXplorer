using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class AddCloudImageToRenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CloudImageId",
                table: "Renters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Renters_CloudImageId",
                table: "Renters",
                column: "CloudImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Renters_CloudImages_CloudImageId",
                table: "Renters",
                column: "CloudImageId",
                principalTable: "CloudImages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Renters_CloudImages_CloudImageId",
                table: "Renters");

            migrationBuilder.DropIndex(
                name: "IX_Renters_CloudImageId",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "CloudImageId",
                table: "Renters");
        }
    }
}
