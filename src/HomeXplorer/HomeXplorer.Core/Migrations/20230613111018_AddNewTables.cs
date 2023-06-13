using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Core.Migrations
{
    public partial class AddNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Name of the agency"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Email of the agency"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Address of the agency")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                },
                comment: "Agency of the agent");

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
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "First name of the agent"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Last name of the agent"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Phone number of the agent"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Email of the agent"),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Rating of the agent"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Reference to the IdentityUser"),
                    AgencyId = table.Column<int>(type: "int", nullable: false, comment: "Agency ID of the agent")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agents_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Agent who offers the property");

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
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    BuildingTypeId = table.Column<int>(type: "int", nullable: false, comment: "Building type ID of the property")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
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
                },
                comment: "Offered property");

            migrationBuilder.CreateTable(
                name: "AgentsProperties",
                columns: table => new
                {
                    AgentId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsProperties", x => new { x.AgentId, x.PropertyId });
                    table.ForeignKey(
                        name: "FK_AgentsProperties_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentsProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AgencyId",
                table: "Agents",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UserId",
                table: "Agents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentsProperties_PropertyId",
                table: "AgentsProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentsProperties");

            migrationBuilder.DropTable(
                name: "PageVisits");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "BuildingTypes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "PropertyStatuses");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
