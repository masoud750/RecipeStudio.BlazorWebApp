using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeStudio.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ImproveManyToManySeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategoryRecipes",
                columns: new[] { "CategoryId", "RecipeId" },
                values: new object[] { 7, 1 });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "IngredientId", "RecipeId" },
                values: new object[,]
                {
                    { 4, 1 },
                    { 5, 1 },
                    { 9, 1 }
                });

            migrationBuilder.InsertData(
                table: "RecipeSteps",
                columns: new[] { "RecipeId", "StepId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryRecipes",
                keyColumns: new[] { "CategoryId", "RecipeId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "RecipeSteps",
                keyColumns: new[] { "RecipeId", "StepId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "RecipeSteps",
                keyColumns: new[] { "RecipeId", "StepId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "RecipeSteps",
                keyColumns: new[] { "RecipeId", "StepId" },
                keyValues: new object[] { 1, 10 });
        }
    }
}
