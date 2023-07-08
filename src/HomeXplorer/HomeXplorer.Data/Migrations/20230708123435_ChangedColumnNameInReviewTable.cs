using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class ChangedColumnNameInReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Renters_ReviewerId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ReviewerId",
                table: "Reviews",
                newName: "ReviewCreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                newName: "IX_Reviews_ReviewCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Renters_ReviewCreatorId",
                table: "Reviews",
                column: "ReviewCreatorId",
                principalTable: "Renters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Renters_ReviewCreatorId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ReviewCreatorId",
                table: "Reviews",
                newName: "ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReviewCreatorId",
                table: "Reviews",
                newName: "IX_Reviews_ReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Renters_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "Renters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
