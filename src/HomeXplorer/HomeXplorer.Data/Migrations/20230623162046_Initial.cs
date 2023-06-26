using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "First name of the user"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Last name of the user"),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Name of the building type")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingTypes", x => x.Id);
                },
                comment: "Building type of the property");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Name of the country")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                },
                comment: "Country of the property");

            migrationBuilder.CreateTable(
                name: "PageVisits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "URL that is being visited"),
                    VisitsCount = table.Column<int>(type: "int", nullable: false, comment: "Count of visits"),
                    HashedVisitCookie = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Hashed value of the cookie")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageVisits", x => x.Id);
                },
                comment: "Page visits");

            migrationBuilder.CreateTable(
                name: "PropertyStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Name of the property status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyStatuses", x => x.Id);
                },
                comment: "Status of the property");

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Name of the property status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                },
                comment: "Type of the property");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Renters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Renters_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Name of the city"),
                    CountryId = table.Column<int>(type: "int", nullable: false, comment: "Country ID of the city")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "City where the property is");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Review description"),
                    ReviewerId = table.Column<int>(type: "int", nullable: false, comment: "Reviewer ID"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Renters_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Renters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Review of a renter");

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Reference to the IdentityUser"),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agents_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                },
                comment: "Agent who offers the property");

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Primary key"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Name of the property"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Price of the property"),
                    Size = table.Column<int>(type: "int", nullable: false, comment: "Size of the property (square meters)"),
                    PetsAllowed = table.Column<bool>(type: "bit", nullable: false, comment: "Allowed/not allowed pets in the property"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Address of the property"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Is the property active or not"),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Time when property offer is being added"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Time when property offer is being edited"),
                    CityId = table.Column<int>(type: "int", nullable: false, comment: "City ID of the property"),
                    PropertyTypeId = table.Column<int>(type: "int", nullable: false, comment: "Type ID of the property"),
                    PropertyStatusId = table.Column<int>(type: "int", nullable: false, comment: "Status ID of the property"),
                    BuildingTypeId = table.Column<int>(type: "int", nullable: false, comment: "Building type ID of the property"),
                    AgentId = table.Column<int>(type: "int", nullable: false, comment: "Agent ID of the property"),
                    RenterId = table.Column<int>(type: "int", nullable: true, comment: "Property renter ID"),
                    RenterId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_BuildingTypes_BuildingTypeId",
                        column: x => x.BuildingTypeId,
                        principalTable: "BuildingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyStatuses_PropertyStatusId",
                        column: x => x.PropertyStatusId,
                        principalTable: "PropertyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Renters_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Renters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Properties_Renters_RenterId1",
                        column: x => x.RenterId1,
                        principalTable: "Renters",
                        principalColumn: "Id");
                },
                comment: "Offered property");

            migrationBuilder.CreateTable(
                name: "CloudImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CloudImages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Bulgaria" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Sofia" },
                    { 2, 1, "Plovdiv" },
                    { 3, 1, "Varna" },
                    { 4, 1, "Burgas" },
                    { 5, 1, "Ruse" },
                    { 6, 1, "Stara Zagora" },
                    { 7, 1, "Pleven" },
                    { 8, 1, "Sliven" },
                    { 9, 1, "Pazardzhik" },
                    { 10, 1, "Pernik" },
                    { 11, 1, "Dobrich" },
                    { 12, 1, "Shumen" },
                    { 13, 1, "Veliko Tarnovo" },
                    { 14, 1, "Haskovo" },
                    { 15, 1, "Blagoevgrad" },
                    { 16, 1, "Yambol" },
                    { 17, 1, "Kazanlak" },
                    { 18, 1, "Asenovgrad" },
                    { 19, 1, "Vratsa" },
                    { 20, 1, "Kyustendil" },
                    { 21, 1, "Gabrovo" },
                    { 22, 1, "Targovishte" },
                    { 23, 1, "Kardzhali" },
                    { 24, 1, "Vidin" },
                    { 25, 1, "Razgrad" },
                    { 26, 1, "Svishtov" },
                    { 27, 1, "Silistra" },
                    { 28, 1, "Lovech" },
                    { 29, 1, "Montana" },
                    { 30, 1, "Dimitrovgrad" },
                    { 31, 1, "Dupnitsa" },
                    { 32, 1, "Smolyan" },
                    { 33, 1, "Gorna Oryahovitsa" },
                    { 34, 1, "Petrich" },
                    { 35, 1, "Gotse Delchev" },
                    { 36, 1, "Aytos" },
                    { 37, 1, "Omurtag" },
                    { 38, 1, "Velingrad" },
                    { 39, 1, "Isperih" },
                    { 40, 1, "Karlovo" },
                    { 41, 1, "Lom" },
                    { 42, 1, "Panagyurishte" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 43, 1, "Botevgrad" },
                    { 44, 1, "Peshtera" },
                    { 45, 1, "Rakovski" },
                    { 46, 1, "Veliki Preslav" },
                    { 47, 1, "Pomorie" },
                    { 48, 1, "Lyaskovets" },
                    { 49, 1, "Novi Pazar" },
                    { 50, 1, "Provadia" },
                    { 51, 1, "Razlog" },
                    { 52, 1, "Zlatograd" },
                    { 53, 1, "Kozloduy" },
                    { 54, 1, "Kostinbrod" },
                    { 55, 1, "Bankya" },
                    { 56, 1, "Stamboliyski" },
                    { 57, 1, "Etropole" },
                    { 58, 1, "Devnya" },
                    { 59, 1, "Rakitovo" },
                    { 60, 1, "Sopot" },
                    { 61, 1, "Septemvri" },
                    { 62, 1, "Krichim" },
                    { 63, 1, "Byala" },
                    { 64, 1, "Aksakovo" },
                    { 65, 1, "Beloslav" },
                    { 66, 1, "Slivnitsa" },
                    { 67, 1, "Elin Pelin" },
                    { 68, 1, "Madan" },
                    { 69, 1, "Aydemir" },
                    { 70, 1, "Devin" },
                    { 71, 1, "Lozen" },
                    { 72, 1, "Varshets" },
                    { 73, 1, "Saedinenie" },
                    { 74, 1, "Bistritsa" },
                    { 75, 1, "Bozhurishte" },
                    { 76, 1, "Suvorovo" },
                    { 77, 1, "Perushtitsa" },
                    { 78, 1, "Dolna Banya" },
                    { 79, 1, "Vetovo" },
                    { 80, 1, "Kazichene" },
                    { 81, 1, "Ignatievo" },
                    { 82, 1, "Kostandovo" },
                    { 83, 1, "Bukovlak" },
                    { 84, 1, "Koynare" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 85, 1, "Slavyanovo" },
                    { 86, 1, "Kalipetrovo" },
                    { 87, 1, "Trud" },
                    { 88, 1, "Sveti Vlas" },
                    { 89, 1, "Sapareva Banya" },
                    { 90, 1, "Malo Konare" },
                    { 91, 1, "Varbitsa" },
                    { 92, 1, "Marten" },
                    { 93, 1, "Debelets" },
                    { 94, 1, "Vladaya" },
                    { 95, 1, "Parvenets" },
                    { 96, 1, "Sarnitsa" },
                    { 97, 1, "Rudozem" },
                    { 98, 1, "Topolchane" },
                    { 99, 1, "Brestovitsa" },
                    { 100, 1, "Gulyantsi" },
                    { 101, 1, "Nikolaevo" },
                    { 102, 1, "Rogosh" },
                    { 103, 1, "Kran" },
                    { 104, 1, "Banya" },
                    { 105, 1, "Topoli" },
                    { 106, 1, "Pancharevo" },
                    { 107, 1, "Kamenar" },
                    { 108, 1, "Gurkovo" },
                    { 109, 1, "Ivaylo" },
                    { 110, 1, "Sotirya" },
                    { 111, 1, "Kableshkovo" },
                    { 112, 1, "Ruen" },
                    { 113, 1, "Vetren" },
                    { 114, 1, "Dolna Oryahovitsa" },
                    { 115, 1, "Buhovo" },
                    { 116, 1, "Kalofer" },
                    { 117, 1, "Voluyak" },
                    { 118, 1, "Kalekovets" },
                    { 119, 1, "German" },
                    { 120, 1, "Nikolovo" },
                    { 121, 1, "Ravda" },
                    { 122, 1, "Glozhene" },
                    { 123, 1, "Novo Selo" },
                    { 124, 1, "Kurtovo Konare" },
                    { 125, 1, "Ablanitsa" },
                    { 126, 1, "Skutare" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 127, 1, "Sadovo" },
                    { 128, 1, "Chepintsi" },
                    { 129, 1, "Parvomaytsi" },
                    { 130, 1, "Draganovo" },
                    { 131, 1, "Kilifarevo" },
                    { 132, 1, "Ognyanovo" },
                    { 133, 1, "Valkosel" },
                    { 134, 1, "Rakovski" },
                    { 135, 1, "Glavinitsa" },
                    { 136, 1, "Bregovo" },
                    { 137, 1, "Svetovrachane" },
                    { 138, 1, "Aheloy" },
                    { 139, 1, "Krushare" },
                    { 140, 1, "Startsevo" },
                    { 141, 1, "Gradina" },
                    { 142, 1, "Obzor" },
                    { 143, 1, "Batanovtsi" },
                    { 144, 1, "Tsaratsovo" },
                    { 145, 1, "Borovo" },
                    { 146, 1, "Voyvodinovo" },
                    { 147, 1, "Chernogorovo" },
                    { 148, 1, "Balgarovo" },
                    { 149, 1, "Petarch" },
                    { 150, 1, "Kokalyane" },
                    { 151, 1, "Dragichevo" },
                    { 152, 1, "Yahinovo" },
                    { 153, 1, "Ostrovo" },
                    { 154, 1, "Mokrishte" },
                    { 155, 1, "Kraynitsi" },
                    { 156, 1, "Kostievo" },
                    { 157, 1, "Resen" },
                    { 158, 1, "Aleksandrovo" },
                    { 159, 1, "Sinitovo" },
                    { 160, 1, "Ezerovo" },
                    { 161, 1, "Graf Ignatievo" },
                    { 162, 1, "Seliminovo" },
                    { 163, 1, "Uzundzhovo" },
                    { 164, 1, "Busmantsi" },
                    { 165, 1, "Polikrayshte" },
                    { 166, 1, "Lesnovo" },
                    { 167, 1, "Merichleri" },
                    { 168, 1, "Lyuben Karavelovo" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 169, 1, "Divotino" },
                    { 170, 1, "Byal Izvor" },
                    { 171, 1, "Kermen" },
                    { 172, 1, "Ravnets" },
                    { 173, 1, "Kukorevo" },
                    { 174, 1, "Samoranovo" },
                    { 175, 1, "Stozher" },
                    { 176, 1, "Dzhulyunitsa" },
                    { 177, 1, "Zheleznitsa" },
                    { 178, 1, "Slokoshtitsa" },
                    { 179, 1, "Orizare" },
                    { 180, 1, "Negovan" },
                    { 181, 1, "Trivoditsi" },
                    { 182, 1, "Krepost" },
                    { 183, 1, "Grivitsa" },
                    { 184, 1, "Studena" },
                    { 185, 1, "Bistritsa" },
                    { 186, 1, "Stratsin" },
                    { 187, 1, "Mirovyane" },
                    { 188, 1, "Saraya" },
                    { 189, 1, "Planinitsa" },
                    { 190, 1, "Krivina" },
                    { 191, 1, "Karageorgievo" },
                    { 192, 1, "Prosenik" },
                    { 193, 1, "Vinogradets" },
                    { 194, 1, "Tsarev Brod" },
                    { 195, 1, "Tankovo" },
                    { 196, 1, "Maglen" },
                    { 197, 1, "Tranak" },
                    { 198, 1, "Razhitsa" },
                    { 199, 1, "Dzherman" },
                    { 200, 1, "Shipka" },
                    { 201, 1, "Basarbovo" },
                    { 202, 1, "Karabunar" },
                    { 203, 1, "Benkovski" },
                    { 204, 1, "Kosharitsa" },
                    { 205, 1, "Konstantinovo" },
                    { 206, 1, "Yabalchevo" },
                    { 207, 1, "Galabets" },
                    { 208, 1, "Chervena Voda" },
                    { 209, 1, "Veselinovo" },
                    { 210, 1, "Bata" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 211, 1, "Rudartsi" },
                    { 212, 1, "Dolni Bogrov" },
                    { 213, 1, "Aldomirovtsi" },
                    { 214, 1, "Belogradets" },
                    { 215, 1, "Karapelit" },
                    { 216, 1, "Novo Selo" },
                    { 217, 1, "Sandrovo" },
                    { 218, 1, "Yabalkovo" },
                    { 219, 1, "Gabrovnitsa" },
                    { 220, 1, "Vaglen" },
                    { 221, 1, "Vardun" },
                    { 222, 1, "Marchaevo" },
                    { 223, 1, "Banitsa" },
                    { 224, 1, "Gorni Bogrov" },
                    { 225, 1, "Semerdzhievo" },
                    { 226, 1, "Zaychar" },
                    { 227, 1, "Voyvodovo" },
                    { 228, 1, "Malevo" },
                    { 229, 1, "Opalchensko" },
                    { 230, 1, "Zvezditsa" },
                    { 231, 1, "Marinka" },
                    { 232, 1, "Boboshevo" },
                    { 233, 1, "Gyulyovtsa" },
                    { 234, 1, "Gluhar" },
                    { 235, 1, "Hadzhievo" },
                    { 236, 1, "Pisarevo" },
                    { 237, 1, "Vaklinovo" },
                    { 238, 1, "Varbitsa" },
                    { 239, 1, "Kladnitsa" },
                    { 240, 1, "Patalenitsa" },
                    { 241, 1, "Dolen" },
                    { 242, 1, "Dobromir" },
                    { 243, 1, "Dobri Dyal" },
                    { 244, 1, "Gorski Izvor" },
                    { 245, 1, "Stefanovo" },
                    { 246, 1, "Borovo" },
                    { 247, 1, "Cherven Breg" },
                    { 248, 1, "Snyagovo" },
                    { 249, 1, "Krastava" },
                    { 250, 1, "Kadievo" },
                    { 251, 1, "Voysil" },
                    { 252, 1, "Vresovo" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 253, 1, "Vinitsa" },
                    { 254, 1, "Mortagonovo" },
                    { 255, 1, "Izvorsko" },
                    { 256, 1, "Dobroslavtsi" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_CityId",
                table: "Agents",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UserId",
                table: "Agents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CloudImages_PropertyId",
                table: "CloudImages",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId",
                table: "Properties",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_BuildingTypeId",
                table: "Properties",
                column: "BuildingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CityId",
                table: "Properties",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyStatusId",
                table: "Properties",
                column: "PropertyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_RenterId",
                table: "Properties",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_RenterId1",
                table: "Properties",
                column: "RenterId1");

            migrationBuilder.CreateIndex(
                name: "IX_Renters_UserId",
                table: "Renters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ApplicationUserId",
                table: "Reviews",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                column: "ReviewerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CloudImages");

            migrationBuilder.DropTable(
                name: "PageVisits");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "BuildingTypes");

            migrationBuilder.DropTable(
                name: "PropertyStatuses");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "Renters");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
