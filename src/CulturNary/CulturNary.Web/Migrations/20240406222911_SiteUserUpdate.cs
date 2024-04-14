using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CulturNary.Web.Migrations
{
    /// <inheritdoc />
    public partial class SiteUserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    identity_id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Person__3213E83F37E58F11", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    person_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    tags = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    img = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Collecti__3213E83FFE9DF17F", x => x.id);
                    table.ForeignKey(
                        name: "FK__Collectio__perso__5629CD9C",
                        column: x => x.person_id,
                        principalTable: "Person",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    collection_id = table.Column<int>(type: "int", nullable: true),
                    person_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    img = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Recipes__3213E83F83A3281A", x => x.id);
                    table.ForeignKey(
                        name: "FK__Recipes__collect__59063A47",
                        column: x => x.collection_id,
                        principalTable: "Collections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Recipes__person___59FA5E80",
                        column: x => x.person_id,
                        principalTable: "Person",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collections_person_id",
                table: "Collections",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Person__D51AF5F55669F3EA",
                table: "Person",
                column: "identity_id",
                unique: true,
                filter: "[identity_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_collection_id",
                table: "Recipes",
                column: "collection_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_person_id",
                table: "Recipes",
                column: "person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
