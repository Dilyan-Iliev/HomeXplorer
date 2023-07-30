using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class FixedRenterSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "92c105b4-cbb1-4f0f-83d7-eabafe920457", "AQAAAAEAACcQAAAAEMxVd+YGDFEd9I/ABBONdKQTuQkA9qbM0IuiCx9vdyeq5/2LeyPUsUvQqqD+bZA/gQ==", new DateTime(2023, 7, 30, 14, 8, 56, 938, DateTimeKind.Utc).AddTicks(2696), "1e65e403-07d5-410e-9dc8-2c2ed94e2442" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a30c9896-54aa-4901-878a-b1bd6417f91e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "8d4bc3be-2202-40b8-be4e-769e4fbeeb12", "AQAAAAEAACcQAAAAEIhSu7wydhjSiDaZoodCm0ZuEJa73TaxLw7/iWAGU9dIIoxDeNt8ResUOsBdIx+9WA==", new DateTime(2023, 7, 30, 14, 8, 56, 928, DateTimeKind.Utc).AddTicks(8452), "948cc8de-1c6c-4a9d-9351-3a17a76f342d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fad56a17-221a-409c-b9aa-5fa0f274f9c0",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedUserName", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "7d6aa63f-4a9f-4234-a622-c79d6905eff8", "rentertest@test.bg", "RENTERTEST@TEST.BG", "AQAAAAEAACcQAAAAEHggyw4QBfimzvUmEMKM56Y2f1DajcRI6pw0YvsBqxOIUKHvEKUfhP4bIIT2VxlPxw==", new DateTime(2023, 7, 30, 14, 8, 56, 948, DateTimeKind.Utc).AddTicks(2481), "9fb2f67e-330e-4d66-8327-5047be45cf35" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedUserName", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "dcce86de-bf92-48a1-8982-0340b6daf3bd", "renterttest@test.bg", "renterTEST@TEST.BG", "AQAAAAEAACcQAAAAEHbzWsKDSOpSCRk3lI2lAECHfvfvS4l721MUNMQde4GPmz7W0yjpork36yEyrHUwlA==", new DateTime(2023, 7, 30, 14, 5, 50, 352, DateTimeKind.Utc).AddTicks(9585), "3fcf72b1-2189-4abf-910b-27dd921ab699" });
        }
    }
}
