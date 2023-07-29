using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Data.Migrations
{
    public partial class Init : Migration
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
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the user is being registered"),
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
                },
                comment: "Extended Identity User");

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
                    VisitsCount = table.Column<int>(type: "int", nullable: false, comment: "Count of visits")
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
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Reference to the IdentityUser"),
                    CityId = table.Column<int>(type: "int", nullable: false, comment: "Reference to the City"),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The profile picture of the agent")
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Agent who offers the property");

            migrationBuilder.CreateTable(
                name: "Renters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Refference to the Identity User"),
                    CityId = table.Column<int>(type: "int", nullable: false, comment: "The associated City"),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Profile picture of the renter")
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
                    table.ForeignKey(
                        name: "FK_Renters_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Renter of the property");

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Primary key"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Name of the property"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Desription of the property"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Price of the property"),
                    Size = table.Column<int>(type: "int", nullable: false, comment: "Size of the property (square meters)"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Address of the property"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Is the property active or not"),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Time when property offer is being added"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Time when property offer is being edited"),
                    CityId = table.Column<int>(type: "int", nullable: false, comment: "City ID of the property"),
                    PropertyTypeId = table.Column<int>(type: "int", nullable: false, comment: "Type ID of the property"),
                    PropertyStatusId = table.Column<int>(type: "int", nullable: false, comment: "Status ID of the property"),
                    BuildingTypeId = table.Column<int>(type: "int", nullable: false, comment: "Building type ID of the property"),
                    AgentId = table.Column<int>(type: "int", nullable: false, comment: "Agent ID of the property"),
                    RenterId = table.Column<int>(type: "int", nullable: true, comment: "Property renter ID")
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
                        principalColumn: "Id");
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
                },
                comment: "Offered property");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Review description"),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the review was added"),
                    ReviewCreatorId = table.Column<int>(type: "int", nullable: false, comment: "Reviewer ID"),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates if the review is approved")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Renters_ReviewCreatorId",
                        column: x => x.ReviewCreatorId,
                        principalTable: "Renters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Review of a renter");

            migrationBuilder.CreateTable(
                name: "CloudImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Url to the cloudinary"),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Property Id of the Image")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CloudImages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                },
                comment: "Image of the property");

            migrationBuilder.CreateTable(
                name: "RentersPropertiesFavorites",
                columns: table => new
                {
                    RenterId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentersPropertiesFavorites", x => new { x.PropertyId, x.RenterId });
                    table.ForeignKey(
                        name: "FK_RentersPropertiesFavorites_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentersPropertiesFavorites_Renters_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Renters",
                        principalColumn: "Id");
                },
                comment: "Linking table representing favorite properties for renters");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1fec1601-56ea-4757-ae65-590e0007a356", "cb14d5e0-d3f3-41ef-8ff5-c13c7b030c30", "Agent", "AGENT" },
                    { "66a2871f-9cc2-4ece-93b9-8ec584db7ed1", "17987d43-6434-48ee-a2d9-3dafa661aa41", "Renter", "Renter" },
                    { "e7c3115f-215e-4f62-a15f-ffa31b0f6ac1", "f5d5297a-1cdd-4236-ae81-bf1e7ff0f75e", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegisteredOn", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55", 0, "d84679c2-bece-47b0-b17e-6a4708be645c", "agenttest@test.bg", false, "Initial", "Agent", false, null, "AGENTTEST@TEST.BG", "AGENTTEST@TEST.BG", "AQAAAAEAACcQAAAAECk7G1enJp+i6liB1luB4X2zrMERokXG9s5yRuaswRZRjmgBnpkkMZfsGqFfyA1nDw==", null, false, new DateTime(2023, 7, 29, 14, 37, 11, 522, DateTimeKind.Utc).AddTicks(1942), "5260ab13-f5a1-4d0c-86c4-e466a024781f", false, "agenttest@test.bg" },
                    { "a30c9896-54aa-4901-878a-b1bd6417f91e", 0, "e1243e4b-253b-488c-8042-2c5666f21188", "applicationtest@abv.bg", false, "Platform", "Admin", false, null, "APPLICATIONTEST@ABV.BG", "APPLICATIONTEST@ABV.BG", "AQAAAAEAACcQAAAAEPvhHAfR7h8xtejl6E6axiLWbwMtzRSJVek/06RDUBHfnEkLA07BWop4PXVr6CO3eg==", null, false, new DateTime(2023, 7, 29, 14, 37, 11, 509, DateTimeKind.Utc).AddTicks(9874), "78e595fc-02ac-48df-b5af-f39f6ed6d8b6", false, "applicationtest@abv.bg" },
                    { "fad56a17-221a-409c-b9aa-5fa0f274f9c0", 0, "226ad7b6-8283-4dcb-aac2-b9a3010e0f28", "renterttest@test.bg", false, "Initial", "Renter", false, null, "RENTERTEST@TEST.BG", "renterTEST@TEST.BG", "AQAAAAEAACcQAAAAELAsUxexZMQtK4J0YYlTdjzY5QtXcNXZm2hmXhmBvy95UOXuPJqZxWuNHMoov4qu2A==", null, false, new DateTime(2023, 7, 29, 14, 37, 11, 536, DateTimeKind.Utc).AddTicks(4810), "c3949aec-dfc3-47bc-8ab6-d413628ed3a6", false, "rentertest@test.bg" }
                });

            migrationBuilder.InsertData(
                table: "BuildingTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Luxury" },
                    { 2, "Average" },
                    { 3, "Ordinary" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Bulgaria" });

            migrationBuilder.InsertData(
                table: "PropertyStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Free" },
                    { 2, "Reserved" },
                    { 3, "Taken" }
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Villa" },
                    { 2, "Single-Family Home" },
                    { 3, "Townhome" },
                    { 4, "Bungalow" },
                    { 5, "Ranch" },
                    { 6, "Studio" },
                    { 7, "Residential area" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1fec1601-56ea-4757-ae65-590e0007a356", "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55" },
                    { "e7c3115f-215e-4f62-a15f-ffa31b0f6ac1", "a30c9896-54aa-4901-878a-b1bd6417f91e" },
                    { "66a2871f-9cc2-4ece-93b9-8ec584db7ed1", "fad56a17-221a-409c-b9aa-5fa0f274f9c0" }
                });

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
                    { 39, 1, "Isperih" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 40, 1, "Karlovo" },
                    { 41, 1, "Lom" },
                    { 42, 1, "Panagyurishte" },
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
                    { 81, 1, "Ignatievo" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 82, 1, "Kostandovo" },
                    { 83, 1, "Bukovlak" },
                    { 84, 1, "Koynare" },
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
                    { 123, 1, "Novo Selo" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 124, 1, "Kurtovo Konare" },
                    { 125, 1, "Ablanitsa" },
                    { 126, 1, "Skutare" },
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
                    { 165, 1, "Polikrayshte" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 166, 1, "Lesnovo" },
                    { 167, 1, "Merichleri" },
                    { 168, 1, "Lyuben Karavelovo" },
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
                    { 207, 1, "Galabets" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 208, 1, "Chervena Voda" },
                    { 209, 1, "Veselinovo" },
                    { 210, 1, "Bata" },
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
                    { 249, 1, "Krastava" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 250, 1, "Kadievo" },
                    { 251, 1, "Voysil" },
                    { 252, 1, "Vresovo" },
                    { 253, 1, "Vinitsa" },
                    { 254, 1, "Mortagonovo" },
                    { 255, 1, "Izvorsko" },
                    { 256, 1, "Dobroslavtsi" }
                });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "CityId", "ProfilePictureUrl", "UserId" },
                values: new object[] { 1, 1, "https://res.cloudinary.com/degtesnvc/image/upload/v1688283726/default-avatar-profile-icon-of-social-media-user-vector_lcoi8s.jpg", "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55" });

            migrationBuilder.InsertData(
                table: "Renters",
                columns: new[] { "Id", "CityId", "ProfilePictureUrl", "UserId" },
                values: new object[] { 1, 1, "https://res.cloudinary.com/degtesnvc/image/upload/v1688283726/default-avatar-profile-icon-of-social-media-user-vector_lcoi8s.jpg", "fad56a17-221a-409c-b9aa-5fa0f274f9c0" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "AddedOn", "Address", "AgentId", "BuildingTypeId", "CityId", "Description", "IsActive", "ModifiedOn", "Name", "Price", "PropertyStatusId", "PropertyTypeId", "RenterId", "Size" },
                values: new object[,]
                {
                    { new Guid("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123 Serenity Lane, Greenhaven, Sofia, 12345", 1, 1, 1, "Welcome to Tranquil Solace Villa, a serene escape nestled amidst lush landscapes. This charming villa offers a perfect blend of luxury and comfort. Immerse yourself in the peaceful ambiance, away from the hustle and bustle, and experience the joy of simple living surrounded by nature's beauty", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tranquil Haven Villa", 1250m, 1, 1, null, 100 },
                    { new Guid("62373c07-f1e7-4813-ba49-bc8a61ad8f26"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "456 Tranquility Road, Woodland Springs, Plovdiv, 67890", 1, 2, 2, "Discover Serenity Woods Retreat, a picturesque hideaway set in a woodland paradise. This enchanting retreat offers a cozy sanctuary where you can unwind and rejuvenate. Embrace the soothing sounds of nature, and let the stress melt away in this delightful haven of tranquility.", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serenity Woods Retreat", 1000m, 1, 4, null, 180 },
                    { new Guid("a9742cc5-14cf-424c-b5a5-f4ecba4e1453"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "789 Enchantment Avenue, Meadowland Heights, Sofia, 54321", 1, 1, 1, "Step into the enchanting world of Enchanted Meadow Chalet, where fairy tales come to life. This whimsical chalet is surrounded by lush meadows, creating a magical ambiance that promises a unique and memorable experience. Indulge in the charm of this extraordinary abode and create cherished memories in this one-of-a-kind retreat.", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enchanted Meadow Chalet", 850m, 1, 1, null, 95 },
                    { new Guid("e22089fd-8c9e-4600-94b7-ad946b779f07"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "987 Harmony Hill, Summitview, Sofia, 24680", 1, 1, 1, "Welcome to Harmony Heights Villa, an exclusive hilltop residence offering breathtaking panoramic views. This luxurious villa combines elegance with the serenity of its elevated location. Enjoy a life of opulence and privacy in this stunning retreat, where you can revel in the beauty of the surroundings while indulging in modern comforts.", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harmony Heights Villa", 1400m, 1, 1, null, 150 }
                });

            migrationBuilder.InsertData(
                table: "CloudImages",
                columns: new[] { "Id", "PropertyId", "Url" },
                values: new object[,]
                {
                    { 1, new Guid("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-1_rk3g0b_umsjgr.webp" },
                    { 2, new Guid("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-14_eabwwr_cmcayy.webp" },
                    { 3, new Guid("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-24_upzvcz_wakzke.webp" },
                    { 4, new Guid("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-13_xmndng_wajp67.webp" },
                    { 5, new Guid("62373c07-f1e7-4813-ba49-bc8a61ad8f26"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554623/krtajna-bali-indonesia-01_pyq5dc_jg4sqh.webp" },
                    { 6, new Guid("62373c07-f1e7-4813-ba49-bc8a61ad8f26"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554623/krtajna-bali-indonesia-02_czzwv0_mofzcq.webp" },
                    { 7, new Guid("62373c07-f1e7-4813-ba49-bc8a61ad8f26"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554623/krtajna-bali-indonesia-10_mqq0yu_lyrena.webp" },
                    { 8, new Guid("a9742cc5-14cf-424c-b5a5-f4ecba4e1453"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554832/3_-_DJI_20230704202223_0050_D_ycntes_sj7twt.webp" },
                    { 9, new Guid("a9742cc5-14cf-424c-b5a5-f4ecba4e1453"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554832/11_-_DJI_20230704192355_0019_D_j1gzep_gozyn7.webp" },
                    { 10, new Guid("a9742cc5-14cf-424c-b5a5-f4ecba4e1453"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554832/1_-_DJI_20230704192923_0029_D_pqslwu_jn21nr.webp" },
                    { 11, new Guid("e22089fd-8c9e-4600-94b7-ad946b779f07"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554994/huerta-grande-la-zubia-granada-spain01_ftjwx5_q2uwxc.webp" },
                    { 12, new Guid("e22089fd-8c9e-4600-94b7-ad946b779f07"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554994/huerta-grande-la-zubia-granada-spain04_cqwfiu_c1piur.webp" },
                    { 13, new Guid("e22089fd-8c9e-4600-94b7-ad946b779f07"), "https://res.cloudinary.com/degtesnvc/image/upload/v1690554994/huerta-grande-la-zubia-granada-spain12_zk2jwn_o6rh3p.webp" }
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
                name: "IX_Renters_CityId",
                table: "Renters",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Renters_UserId",
                table: "Renters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentersPropertiesFavorites_RenterId",
                table: "RentersPropertiesFavorites",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewCreatorId",
                table: "Reviews",
                column: "ReviewCreatorId");
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
                name: "RentersPropertiesFavorites");

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
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
