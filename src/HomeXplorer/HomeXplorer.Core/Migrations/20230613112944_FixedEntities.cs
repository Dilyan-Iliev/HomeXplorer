using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeXplorer.Core.Migrations
{
    public partial class FixedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentsProperties");

            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Agent ID of the property");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId",
                table: "Properties",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agents_AgentId",
                table: "Properties",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agents_AgentId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AgentId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Properties");

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
                name: "IX_AgentsProperties_PropertyId",
                table: "AgentsProperties",
                column: "PropertyId");
        }
    }
}
