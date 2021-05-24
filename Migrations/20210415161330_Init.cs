using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttributeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_AttributeTypeId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Precedents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_PrecedentId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clusters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num = table.Column<int>(type: "int", nullable: false),
                    AttributeTypeId = table.Column<int>(type: "int", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_ClusterId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clusters_AttributeTypes_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalTable: "AttributeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecedentId = table.Column<int>(type: "int", nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: false),
                    AttributeTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_AttributeId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attributes_AttributeTypes_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalTable: "AttributeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attributes_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attributes_Precedents_PrecedentId",
                        column: x => x.PrecedentId,
                        principalTable: "Precedents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_AttributeTypeId",
                table: "Attributes",
                column: "AttributeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ClusterId",
                table: "Attributes",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_PrecedentId",
                table: "Attributes",
                column: "PrecedentId");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_AttributeTypeId",
                table: "Clusters",
                column: "AttributeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Clusters");

            migrationBuilder.DropTable(
                name: "Precedents");

            migrationBuilder.DropTable(
                name: "AttributeTypes");
        }
    }
}
