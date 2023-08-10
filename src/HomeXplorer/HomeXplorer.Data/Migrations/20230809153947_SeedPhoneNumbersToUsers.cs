using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class SeedPhoneNumbersToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "04366ced-0998-497f-8ed9-23fb6f626cfc", "AQAAAAEAACcQAAAAEP8Giy64O3T9K+6m68KvphEqsedE1XPUpYjmA0NFXzolE+yZDa2SEXnbDxcrvYFiJg==", "0888888889", new DateTime(2023, 8, 9, 15, 39, 45, 208, DateTimeKind.Utc).AddTicks(4098), "93d3bc3e-add8-40ef-bda9-dbc311710137" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a30c9896-54aa-4901-878a-b1bd6417f91e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "444e72e1-b626-4063-b067-5f75583160b9", "AQAAAAEAACcQAAAAEO2X+4qvraiuvBslMDi4kxkPi4KDlWcO8C1zg2jGY/E6a/AYwPFG3lR/kw41blhuyA==", "0888888888", new DateTime(2023, 8, 9, 15, 39, 45, 199, DateTimeKind.Utc).AddTicks(2221), "2a1b0398-c366-4711-92de-3ccffb9d46c3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fad56a17-221a-409c-b9aa-5fa0f274f9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "0b5e5f61-b564-4b8c-8963-4b8ccd0ae704", "AQAAAAEAACcQAAAAEGunHNS/lxZ2HuIcPNuB4cyqSXMldMy9a3rcsUW5qllXsn0ytX2uOFmh5hlCXMYwag==", "0888888810", new DateTime(2023, 8, 9, 15, 39, 45, 217, DateTimeKind.Utc).AddTicks(6155), "709a52a2-193b-4934-a286-105d49e466ae" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799"),
                column: "AddedOn",
                value: new DateTime(2023, 8, 9, 15, 39, 45, 231, DateTimeKind.Utc).AddTicks(1633));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("62373c07-f1e7-4813-ba49-bc8a61ad8f26"),
                column: "AddedOn",
                value: new DateTime(2023, 8, 9, 15, 39, 45, 231, DateTimeKind.Utc).AddTicks(1657));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("a9742cc5-14cf-424c-b5a5-f4ecba4e1453"),
                column: "AddedOn",
                value: new DateTime(2023, 8, 9, 15, 39, 45, 231, DateTimeKind.Utc).AddTicks(1662));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("e22089fd-8c9e-4600-94b7-ad946b779f07"),
                column: "AddedOn",
                value: new DateTime(2023, 8, 9, 15, 39, 45, 231, DateTimeKind.Utc).AddTicks(1665));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "7d725a52-f82b-47ec-be97-8d4b53f311d9", "AQAAAAEAACcQAAAAEEZkE41vYIAUT1Owzddy4Os6+Yjp/Bve4bTWRlYlSWtzGO0LE4cRjBlpzShNAkVEdg==", null, new DateTime(2023, 8, 3, 11, 26, 37, 552, DateTimeKind.Utc).AddTicks(7069), "0d11bd69-3129-4532-b97b-eaaf0032b3ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a30c9896-54aa-4901-878a-b1bd6417f91e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "e6c4b745-3947-46b1-baeb-ee5c36f1d608", "AQAAAAEAACcQAAAAEJ8FsVeiHmE/TrT8kTh5kxARQbe0BgsvGvVX8PTOiJlzW4TeHlDzzGFION48AHHhnA==", null, new DateTime(2023, 8, 3, 11, 26, 37, 543, DateTimeKind.Utc).AddTicks(7606), "7b61e069-f8cb-4064-bd43-ed6793e3039a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fad56a17-221a-409c-b9aa-5fa0f274f9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "8d3324ff-07b7-4cd0-bb63-5247d0dbd126", "AQAAAAEAACcQAAAAEAmNl7wLVznSarmekhDRPFJDpmfe4iOHKSswdY8hqaEWOw4Kb7Knb6eTwQUIh8buHA==", null, new DateTime(2023, 8, 3, 11, 26, 37, 561, DateTimeKind.Utc).AddTicks(7282), "cf54f6b9-c151-4555-8afa-0bb0fc174de1" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799"),
                column: "AddedOn",
                value: new DateTime(2023, 8, 3, 11, 26, 37, 573, DateTimeKind.Utc).AddTicks(8277));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("62373c07-f1e7-4813-ba49-bc8a61ad8f26"),
                column: "AddedOn",
                value: new DateTime(2023, 8, 3, 11, 26, 37, 573, DateTimeKind.Utc).AddTicks(8288));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("a9742cc5-14cf-424c-b5a5-f4ecba4e1453"),
                column: "AddedOn",
                value: new DateTime(2023, 8, 3, 11, 26, 37, 573, DateTimeKind.Utc).AddTicks(8305));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("e22089fd-8c9e-4600-94b7-ad946b779f07"),
                column: "AddedOn",
                value: new DateTime(2023, 8, 3, 11, 26, 37, 573, DateTimeKind.Utc).AddTicks(8310));
        }
    }
}
