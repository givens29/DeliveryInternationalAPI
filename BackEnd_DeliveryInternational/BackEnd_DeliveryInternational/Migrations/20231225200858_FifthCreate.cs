using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_DeliveryInternational.Migrations
{
    /// <inheritdoc />
    public partial class FifthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Order_orderid",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_Userid",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Userid",
                table: "Orders",
                newName: "IX_Orders_Userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Orders_orderid",
                table: "Carts",
                column: "orderid",
                principalTable: "Orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_Userid",
                table: "Orders",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Orders_orderid",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_Userid",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Userid",
                table: "Order",
                newName: "IX_Order_Userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Order_orderid",
                table: "Carts",
                column: "orderid",
                principalTable: "Order",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_Userid",
                table: "Order",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id");
        }
    }
}
