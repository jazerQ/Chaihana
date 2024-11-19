using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingridients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingridients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DishIngridients",
                columns: table => new
                {
                    DishId = table.Column<int>(type: "int", nullable: false),
                    IngridiendId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishIngridients", x => new { x.DishId, x.IngridiendId });
                    table.ForeignKey(
                        name: "FK_DishIngridients_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishIngridients_Ingridients_IngridiendId",
                        column: x => x.IngridiendId,
                        principalTable: "Ingridients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Name", "imageUrl", "price" },
                values: new object[] { 1, "Pilaf", "https://happylates.com/wp-content/uploads/2014/11/tashkentskiy-plov-1.jpg", 500.0 });

            migrationBuilder.InsertData(
                table: "Ingridients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mutton" },
                    { 2, "Rice" },
                    { 3, "Carrot" },
                    { 4, "Onion" },
                    { 5, "Garlic" },
                    { 6, "Cumin" }
                });

            migrationBuilder.InsertData(
                table: "DishIngridients",
                columns: new[] { "DishId", "IngridiendId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 1, 2, 2 },
                    { 1, 3, 3 },
                    { 1, 4, 4 },
                    { 1, 5, 5 },
                    { 1, 6, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishIngridients_IngridiendId",
                table: "DishIngridients",
                column: "IngridiendId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishIngridients");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Ingridients");
        }
    }
}
