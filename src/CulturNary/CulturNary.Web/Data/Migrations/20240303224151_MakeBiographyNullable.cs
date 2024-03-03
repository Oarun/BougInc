using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CulturNary.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeBiographyNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "Biography",
            table: "AspNetUsers",
            nullable: true,
            oldClrType: typeof(string),
            oldNullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "Biography",
            table: "AspNetUsers",
            nullable: false,
            oldClrType: typeof(string),
            oldNullable: true);
        }
    }
}
