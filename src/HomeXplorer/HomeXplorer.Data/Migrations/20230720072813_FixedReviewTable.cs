using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class FixedReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates if the review is approved");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a30c9896-54aa-4901-878a-b1bd6417f91e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "d6d48432-9c8d-4c54-9133-89b591b55767", "AQAAAAEAACcQAAAAEM0BP/uwbwisbp9ybd79Q9+tWiof46x7/1GzbNcWKzoa5HQdl8SbIKBPVy3UdnAAlA==", new DateTime(2023, 7, 20, 7, 28, 12, 386, DateTimeKind.Utc).AddTicks(4071), "04dedea6-34b0-433a-ad3f-5e601883f08a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a30c9896-54aa-4901-878a-b1bd6417f91e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "ede945b6-99ea-4532-b1e0-033990354bba", "AQAAAAEAACcQAAAAEKZ4rMemeLkpW9zkE+gSuRWi1m8OptD0g1umci9gnn3UrwWaZ0huYRjH/DcQtg3DvQ==", new DateTime(2023, 7, 18, 14, 49, 58, 349, DateTimeKind.Utc).AddTicks(6719), "5f25bbf2-fca0-4b5c-8860-69e8dd527f57" });
        }
    }
}
