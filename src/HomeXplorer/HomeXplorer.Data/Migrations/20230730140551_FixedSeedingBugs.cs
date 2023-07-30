using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class FixedSeedingBugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66a2871f-9cc2-4ece-93b9-8ec584db7ed1",
                column: "NormalizedName",
                value: "RENTER");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "917dde6f-c1c8-42fa-87d7-2cf3ac3c2af1", "AQAAAAEAACcQAAAAEJlUZt/wTxQFCTOPASO02BonflraHqimx9XCLp/h+6kOlRIIsYzD7MDXsXiV8yjbPg==", new DateTime(2023, 7, 30, 14, 5, 50, 342, DateTimeKind.Utc).AddTicks(4253), "e32e83d3-ec16-4279-81ee-d019b01d7caf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a30c9896-54aa-4901-878a-b1bd6417f91e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "2eb4435a-2d29-46d1-afe3-9aa8b749447c", "AQAAAAEAACcQAAAAEAMpIqa/zkspf36XY9B8mnCA3OPF9vhikFRWJqx8JidRh6NQd49zwtvPaaCgIoNDjw==", new DateTime(2023, 7, 30, 14, 5, 50, 333, DateTimeKind.Utc).AddTicks(5656), "3e2dfcb4-5a6e-4d2e-914b-41e85833fd6c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fad56a17-221a-409c-b9aa-5fa0f274f9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "dcce86de-bf92-48a1-8982-0340b6daf3bd", "AQAAAAEAACcQAAAAEHbzWsKDSOpSCRk3lI2lAECHfvfvS4l721MUNMQde4GPmz7W0yjpork36yEyrHUwlA==", new DateTime(2023, 7, 30, 14, 5, 50, 352, DateTimeKind.Utc).AddTicks(9585), "3fcf72b1-2189-4abf-910b-27dd921ab699" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66a2871f-9cc2-4ece-93b9-8ec584db7ed1",
                column: "NormalizedName",
                value: "Renter");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "d84679c2-bece-47b0-b17e-6a4708be645c", "AQAAAAEAACcQAAAAECk7G1enJp+i6liB1luB4X2zrMERokXG9s5yRuaswRZRjmgBnpkkMZfsGqFfyA1nDw==", new DateTime(2023, 7, 29, 14, 37, 11, 522, DateTimeKind.Utc).AddTicks(1942), "5260ab13-f5a1-4d0c-86c4-e466a024781f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a30c9896-54aa-4901-878a-b1bd6417f91e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "e1243e4b-253b-488c-8042-2c5666f21188", "AQAAAAEAACcQAAAAEPvhHAfR7h8xtejl6E6axiLWbwMtzRSJVek/06RDUBHfnEkLA07BWop4PXVr6CO3eg==", new DateTime(2023, 7, 29, 14, 37, 11, 509, DateTimeKind.Utc).AddTicks(9874), "78e595fc-02ac-48df-b5af-f39f6ed6d8b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fad56a17-221a-409c-b9aa-5fa0f274f9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "226ad7b6-8283-4dcb-aac2-b9a3010e0f28", "AQAAAAEAACcQAAAAELAsUxexZMQtK4J0YYlTdjzY5QtXcNXZm2hmXhmBvy95UOXuPJqZxWuNHMoov4qu2A==", new DateTime(2023, 7, 29, 14, 37, 11, 536, DateTimeKind.Utc).AddTicks(4810), "c3949aec-dfc3-47bc-8ab6-d413628ed3a6" });
        }
    }
}
