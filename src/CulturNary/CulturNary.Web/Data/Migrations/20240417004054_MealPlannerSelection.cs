using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CulturNary.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class MealPlannerSelection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MealPlans",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealPlans",
                table: "AspNetUsers");
        }
    }
}
