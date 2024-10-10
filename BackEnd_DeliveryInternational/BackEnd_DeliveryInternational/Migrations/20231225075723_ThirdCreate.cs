using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_DeliveryInternational.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dish_Users_Userid",
                table: "Dish");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Dish_Dishid",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dish",
                table: "Dish");

            migrationBuilder.RenameTable(
                name: "Dish",
                newName: "Dishes");

            migrationBuilder.RenameIndex(
                name: "IX_Dish_Userid",
                table: "Dishes",
                newName: "IX_Dishes_Userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dishes",
                table: "Dishes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Users_Userid",
                table: "Dishes",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Dishes_Dishid",
                table: "Rating",
                column: "Dishid",
                principalTable: "Dishes",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Users_Userid",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Dishes_Dishid",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dishes",
                table: "Dishes");

            migrationBuilder.RenameTable(
                name: "Dishes",
                newName: "Dish");

            migrationBuilder.RenameIndex(
                name: "IX_Dishes_Userid",
                table: "Dish",
                newName: "IX_Dish_Userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dish",
                table: "Dish",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dish_Users_Userid",
                table: "Dish",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Dish_Dishid",
                table: "Rating",
                column: "Dishid",
                principalTable: "Dish",
                principalColumn: "id");
        }
    }
}
