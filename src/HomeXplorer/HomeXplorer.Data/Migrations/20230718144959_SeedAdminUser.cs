using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class SeedAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7c3115f-215e-4f62-a15f-ffa31b0f6ac1", "f5d5297a-1cdd-4236-ae81-bf1e7ff0f75e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegisteredOn", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a30c9896-54aa-4901-878a-b1bd6417f91e", 0, "ede945b6-99ea-4532-b1e0-033990354bba", "applicationtest@abv.bg", false, "Platform", "Admin", false, null, "APPLICATIONTEST@ABV.BG", "APPLICATIONTEST@ABV.BG", "AQAAAAEAACcQAAAAEKZ4rMemeLkpW9zkE+gSuRWi1m8OptD0g1umci9gnn3UrwWaZ0huYRjH/DcQtg3DvQ==", null, false, new DateTime(2023, 7, 18, 14, 49, 58, 349, DateTimeKind.Utc).AddTicks(6719), "5f25bbf2-fca0-4b5c-8860-69e8dd527f57", false, "applicationtest@abv.bg" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e7c3115f-215e-4f62-a15f-ffa31b0f6ac1", "a30c9896-54aa-4901-878a-b1bd6417f91e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e7c3115f-215e-4f62-a15f-ffa31b0f6ac1", "a30c9896-54aa-4901-878a-b1bd6417f91e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7c3115f-215e-4f62-a15f-ffa31b0f6ac1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a30c9896-54aa-4901-878a-b1bd6417f91e");
        }
    }
}
