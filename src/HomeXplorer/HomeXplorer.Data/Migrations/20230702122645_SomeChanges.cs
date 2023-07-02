using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class SomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_CloudImages_CloudImageId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Renters_CloudImages_CloudImageId",
                table: "Renters");

            migrationBuilder.DropIndex(
                name: "IX_Renters_CloudImageId",
                table: "Renters");

            migrationBuilder.DropIndex(
                name: "IX_Agents_CloudImageId",
                table: "Agents");

            migrationBuilder.DeleteData(
                table: "CloudImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CloudImageId",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "CloudImageId",
                table: "Agents");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Agents");

            migrationBuilder.AddColumn<int>(
                name: "CloudImageId",
                table: "Renters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CloudImageId",
                table: "Agents",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "CloudImages",
                columns: new[] { "Id", "PropertyId", "Url" },
                values: new object[] { 1, null, "https://res.cloudinary.com/degtesnvc/image/upload/v1688283726/default-avatar-profile-icon-of-social-media-user-vector_lcoi8s.jpg" });

            migrationBuilder.CreateIndex(
                name: "IX_Renters_CloudImageId",
                table: "Renters",
                column: "CloudImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_CloudImageId",
                table: "Agents",
                column: "CloudImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_CloudImages_CloudImageId",
                table: "Agents",
                column: "CloudImageId",
                principalTable: "CloudImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Renters_CloudImages_CloudImageId",
                table: "Renters",
                column: "CloudImageId",
                principalTable: "CloudImages",
                principalColumn: "Id");
        }
    }
}
