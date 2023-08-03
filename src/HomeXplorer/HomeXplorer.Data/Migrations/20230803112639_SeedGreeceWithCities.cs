using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class SeedGreeceWithCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "7d725a52-f82b-47ec-be97-8d4b53f311d9", "AQAAAAEAACcQAAAAEEZkE41vYIAUT1Owzddy4Os6+Yjp/Bve4bTWRlYlSWtzGO0LE4cRjBlpzShNAkVEdg==", new DateTime(2023, 8, 3, 11, 26, 37, 552, DateTimeKind.Utc).AddTicks(7069), "0d11bd69-3129-4532-b97b-eaaf0032b3ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a30c9896-54aa-4901-878a-b1bd6417f91e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "e6c4b745-3947-46b1-baeb-ee5c36f1d608", "AQAAAAEAACcQAAAAEJ8FsVeiHmE/TrT8kTh5kxARQbe0BgsvGvVX8PTOiJlzW4TeHlDzzGFION48AHHhnA==", new DateTime(2023, 8, 3, 11, 26, 37, 543, DateTimeKind.Utc).AddTicks(7606), "7b61e069-f8cb-4064-bd43-ed6793e3039a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fad56a17-221a-409c-b9aa-5fa0f274f9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisteredOn", "SecurityStamp" },
                values: new object[] { "8d3324ff-07b7-4cd0-bb63-5247d0dbd126", "AQAAAAEAACcQAAAAEAmNl7wLVznSarmekhDRPFJDpmfe4iOHKSswdY8hqaEWOw4Kb7Knb6eTwQUIh8buHA==", new DateTime(2023, 8, 3, 11, 26, 37, 561, DateTimeKind.Utc).AddTicks(7282), "cf54f6b9-c151-4555-8afa-0bb0fc174de1" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Greece" });

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

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 257, 2, "Athens" },
                    { 258, 2, "Thessaloníki" },
                    { 259, 2, "Piraeus" },
                    { 260, 2, "Irákleio" },
                    { 261, 2, "Pátra" },
                    { 262, 2, "Lárisa" },
                    { 263, 2, "Vólos" },
                    { 264, 2, "Peristéri" },
                    { 265, 2, "Ioánnina" },
                    { 266, 2, "Acharnés" },
                    { 267, 2, "Kallithéa" },
                    { 268, 2, "Kalamariá" },
                    { 269, 2, "Níkaia" },
                    { 270, 2, "Glyfáda" },
                    { 271, 2, "Ílion" },
                    { 272, 2, "Ilioúpoli" },
                    { 273, 2, "Keratsíni" },
                    { 274, 2, "Évosmos" },
                    { 275, 2, "Chalándri" },
                    { 276, 2, "Néa Smýrni" },
                    { 277, 2, "Maroúsi" },
                    { 278, 2, "Ágios Dimítrios" },
                    { 279, 2, "Zográfos" },
                    { 280, 2, "Aigáleo" },
                    { 281, 2, "Kozáni" },
                    { 282, 2, "Néa Ionía" },
                    { 283, 2, "Kavála" },
                    { 284, 2, "Palaió Fáliro" },
                    { 285, 2, "Korydallós" },
                    { 286, 2, "Agía Paraskeví" },
                    { 287, 2, "Výronas" },
                    { 288, 2, "Galátsi" },
                    { 289, 2, "Chalkída" },
                    { 290, 2, "Petroúpoli" },
                    { 291, 2, "Ródos" },
                    { 292, 2, "Pallíni" },
                    { 293, 2, "Chaniá" },
                    { 294, 2, "Thérmi" },
                    { 295, 2, "Kalamáta" },
                    { 296, 2, "Lamía" },
                    { 297, 2, "Komotiní" },
                    { 298, 2, "Irákleio" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 299, 2, "Kifisiá" },
                    { 300, 2, "Stavroúpoli" },
                    { 301, 2, "Chaïdári" },
                    { 302, 2, "Álimos" },
                    { 303, 2, "Oraiókastro" },
                    { 304, 2, "Sykiés" },
                    { 305, 2, "Ampelókipoi" },
                    { 306, 2, "Pylaía" },
                    { 307, 2, "Ágioi Anárgyroi" },
                    { 308, 2, "Argyroúpoli" },
                    { 309, 2, "Áno Liósia" },
                    { 310, 2, "Ptolemaḯda" },
                    { 311, 2, "Salamína" },
                    { 312, 2, "Trípoli" },
                    { 313, 2, "Cholargós" },
                    { 314, 2, "Vrilíssia" },
                    { 315, 2, "Asprópyrgos" },
                    { 316, 2, "Kórinthos" },
                    { 317, 2, "Gérakas" },
                    { 318, 2, "Metamórfosi" },
                    { 319, 2, "Giannitsá" },
                    { 320, 2, "Amaliáda" },
                    { 321, 2, "Voúla" },
                    { 322, 2, "Kamateró" },
                    { 323, 2, "Mytilíni" },
                    { 324, 2, "Ágios Nikólaos" },
                    { 325, 2, "Chíos" },
                    { 326, 2, "Paianía" },
                    { 327, 2, "Agía Varvára" },
                    { 328, 2, "Kaisarianí" },
                    { 329, 2, "Grevená" },
                    { 330, 2, "Néa Filadélfeia" },
                    { 331, 2, "Moscháto" },
                    { 332, 2, "Pérama" },
                    { 333, 2, "Elefsína" },
                    { 334, 2, "Kérkyra" },
                    { 335, 2, "Pýrgos" },
                    { 336, 2, "Kilkís" },
                    { 337, 2, "Dáfni" },
                    { 338, 2, "Melíssia" },
                    { 339, 2, "Árgos" },
                    { 340, 2, "Árta" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 341, 2, "Artémida" },
                    { 342, 2, "Péfki" },
                    { 343, 2, "Aígio" },
                    { 344, 2, "Gázi" },
                    { 345, 2, "Koropí" },
                    { 346, 2, "Peraía" },
                    { 347, 2, "Orestiáda" },
                    { 348, 2, "Édessa" },
                    { 349, 2, "Flórina" },
                    { 350, 2, "Panórama" },
                    { 351, 2, "Néa Erythraía" },
                    { 352, 2, "Ellinikó" },
                    { 353, 2, "Néa Mákri" },
                    { 354, 2, "Spárti" },
                    { 355, 2, "Ágios Ioánnis Réntis" },
                    { 356, 2, "Vári" },
                    { 357, 2, "Távros" },
                    { 358, 2, "Alexándreia" },
                    { 359, 2, "Néa Alikarnassós" },
                    { 360, 2, "Kalývia Thorikoú" },
                    { 361, 2, "Náfplio" },
                    { 362, 2, "Drapetsóna" },
                    { 363, 2, "Efkarpía" },
                    { 364, 2, "Ermoúpoli" },
                    { 365, 2, "Papágos" },
                    { 366, 2, "Náfpaktos" },
                    { 367, 2, "Kastoriá" },
                    { 368, 2, "Péfka" },
                    { 369, 2, "Ierápetra" },
                    { 370, 2, "Rafína" },
                    { 371, 2, "Ialysós" },
                    { 372, 2, "Týrnavos" },
                    { 373, 2, "Glyká Nerá" },
                    { 374, 2, "Ymittós" },
                    { 375, 2, "Néo Psychikó" },
                    { 376, 2, "Diavatá" },
                    { 377, 2, "Kiáto" },
                    { 378, 2, "Anatolí" },
                    { 379, 2, "Lykóvrysi" },
                    { 380, 2, "Pórto Ráfti" },
                    { 381, 2, "Psychikó" },
                    { 382, 2, "Néa Artáki" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 383, 2, "Zefýri" },
                    { 384, 2, "Néa Moudaniá" },
                    { 385, 2, "Fársala" },
                    { 386, 2, "Síndos" },
                    { 387, 2, "Vochaïkó" },
                    { 388, 2, "Spáta" },
                    { 389, 2, "Chrysoúpoli" },
                    { 390, 2, "Néa Michanióna" },
                    { 391, 2, "Néa Péramos" },
                    { 392, 2, "Giánnouli" },
                    { 393, 2, "Lagkadás" },
                    { 394, 2, "Mourniés" },
                    { 395, 2, "Néa Kallikráteia" },
                    { 396, 2, "Lávrio" },
                    { 397, 2, "Aígina" },
                    { 398, 2, "Néo Karlovási" },
                    { 399, 2, "Káto Achaḯa" },
                    { 400, 2, "Aridaía" },
                    { 401, 2, "Soúda" },
                    { 402, 2, "Kounoupidianá" },
                    { 403, 2, "Anávyssos" },
                    { 404, 2, "Messíni" },
                    { 405, 2, "Néoi Epivátes" },
                    { 406, 2, "Megalópoli" },
                    { 407, 2, "Mýrina" },
                    { 408, 2, "Diónysos" },
                    { 409, 2, "Nerokoúros" },
                    { 410, 2, "Xylókastro" },
                    { 411, 2, "Fílyro" },
                    { 412, 2, "Kremastí" },
                    { 413, 2, "Tympáki" },
                    { 414, 2, "Río" },
                    { 415, 2, "Krýa Vrýsi" },
                    { 416, 2, "Agriá" },
                    { 417, 2, "Kyparissía" },
                    { 418, 2, "Kárystos" },
                    { 419, 2, "Alykés" },
                    { 420, 2, "Alivéri" },
                    { 421, 2, "Tínos" },
                    { 422, 2, "Magoúla" },
                    { 423, 2, "Ampelákia" },
                    { 424, 2, "Filippiáda" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 425, 2, "Itéa" },
                    { 426, 2, "Arkalochóri" },
                    { 427, 2, "Néa Magnisía" },
                    { 428, 2, "Kíssamos" },
                    { 429, 2, "Vouliagméni" },
                    { 430, 2, "Kítsi" },
                    { 431, 2, "Schimatári" },
                    { 432, 2, "Spétses" },
                    { 433, 2, "Katsikás" },
                    { 434, 2, "Néa Raidestós" },
                    { 435, 2, "Potamós" },
                    { 436, 2, "Irákleia" },
                    { 437, 2, "Lixoúri" },
                    { 438, 2, "Stavrós" },
                    { 439, 2, "Amýntaio" },
                    { 440, 2, "Vartholomió" },
                    { 441, 2, "Lagyná" },
                    { 442, 2, "Néa Péramos" },
                    { 443, 2, "Vracháti" },
                    { 444, 2, "Thásos" },
                    { 445, 2, "Mália" },
                    { 446, 2, "Zacháro" },
                    { 447, 2, "Kallithéa" },
                    { 448, 2, "Vélo" },
                    { 449, 2, "Neápoli" },
                    { 450, 2, "Ntráfi" },
                    { 451, 2, "Maniákoi" },
                    { 452, 2, "Liménas Chersonísou" },
                    { 453, 2, "Néo Rýsi" },
                    { 454, 2, "Lefkímmi" },
                    { 455, 2, "Saronída" },
                    { 456, 2, "Néa Kíos" },
                    { 457, 2, "Kárpathos" },
                    { 458, 2, "Spercheiáda" },
                    { 459, 2, "Kanaláki" },
                    { 460, 2, "Filiátes" },
                    { 461, 2, "Mouzáki" },
                    { 462, 2, "Sámos" },
                    { 463, 2, "Kréstena" },
                    { 464, 2, "Firá" },
                    { 465, 2, "Archaía Olympía" },
                    { 466, 2, "Pezá" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 467, 2, "Zákynthos" },
                    { 468, 2, "Ágios Stéfanos" },
                    { 469, 2, "Náxos" },
                    { 470, 2, "Zevgolatió" },
                    { 471, 2, "Moíres" },
                    { 472, 2, "Pérama" },
                    { 473, 2, "Igoumenítsa" },
                    { 474, 2, "Goúrnes" },
                    { 475, 2, "Gastoúni" },
                    { 476, 2, "Eleoúsa" },
                    { 477, 2, "Árgos Orestikó" },
                    { 478, 2, "Markópoulo" },
                    { 479, 2, "Geráni" },
                    { 480, 2, "Ándros" },
                    { 481, 2, "Stylída" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 399);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 430);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 431);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 435);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 437);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 438);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 439);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 443);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 451);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 453);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 455);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 473);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 474);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 475);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 476);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 477);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 478);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

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
    }
}
