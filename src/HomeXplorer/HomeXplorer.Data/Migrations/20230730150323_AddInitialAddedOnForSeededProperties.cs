using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class AddInitialAddedOnForSeededProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "27beb961-b7b0-49c6-8ae9-a36c78f58eea", "AQAAAAEAACcQAAAAEOUZENYQmhxsF7CWjq2V57AE+tq6lF8JrcaIpNNxEAx06wEqRhjPI4d6/9tJxznmcQ==", new DateTime(2023, 7, 30, 15, 3, 22, 652, DateTimeKind.Utc).AddTicks(5732), "42a3caf5-d4e0-44b2-9c30-093869f9313a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a30c9896-54aa-4901-878a-b1bd6417f91e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "3348ad3e-bb80-4bb9-b151-e702bd80ccf0", "AQAAAAEAACcQAAAAEG+77uMSQdHK/VE3CFIb8tQRPCFjGBDGYscCdQy3ZR6tBMNBxokrjVWTQ4/DqOnzOQ==", new DateTime(2023, 7, 30, 15, 3, 22, 643, DateTimeKind.Utc).AddTicks(3516), "b9651497-fc8c-467c-a960-cb345f5b06eb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fad56a17-221a-409c-b9aa-5fa0f274f9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "7b249026-fca2-47c4-9065-8747c50f2c72", "AQAAAAEAACcQAAAAEBF0SbBIebixV1Upn8i/KTwdmw0XZF9KgRUoNz4vCAMME2f89MGYm53bBIK1+4ZSPQ==", new DateTime(2023, 7, 30, 15, 3, 22, 663, DateTimeKind.Utc).AddTicks(6206), "df38ecac-8374-4f18-ae6b-3542ce5fcf6b" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799"),
                column: "AddedOn",
                value: new DateTime(2023, 7, 30, 15, 3, 22, 676, DateTimeKind.Utc).AddTicks(374));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("62373c07-f1e7-4813-ba49-bc8a61ad8f26"),
                column: "AddedOn",
                value: new DateTime(2023, 7, 30, 15, 3, 22, 676, DateTimeKind.Utc).AddTicks(384));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("a9742cc5-14cf-424c-b5a5-f4ecba4e1453"),
                column: "AddedOn",
                value: new DateTime(2023, 7, 30, 15, 3, 22, 676, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("e22089fd-8c9e-4600-94b7-ad946b779f07"),
                column: "AddedOn",
                value: new DateTime(2023, 7, 30, 15, 3, 22, 676, DateTimeKind.Utc).AddTicks(395));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "7d6aa63f-4a9f-4234-a622-c79d6905eff8", "AQAAAAEAACcQAAAAEHggyw4QBfimzvUmEMKM56Y2f1DajcRI6pw0YvsBqxOIUKHvEKUfhP4bIIT2VxlPxw==", new DateTime(2023, 7, 30, 14, 8, 56, 948, DateTimeKind.Utc).AddTicks(2481), "9fb2f67e-330e-4d66-8327-5047be45cf35" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799"),
                column: "AddedOn",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("62373c07-f1e7-4813-ba49-bc8a61ad8f26"),
                column: "AddedOn",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("a9742cc5-14cf-424c-b5a5-f4ecba4e1453"),
                column: "AddedOn",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("e22089fd-8c9e-4600-94b7-ad946b779f07"),
                column: "AddedOn",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
