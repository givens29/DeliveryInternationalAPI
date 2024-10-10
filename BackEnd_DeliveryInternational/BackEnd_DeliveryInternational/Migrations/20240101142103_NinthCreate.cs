using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_DeliveryInternational.Migrations
{
    /// <inheritdoc />
    public partial class NinthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Orders_Orderid",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_Orderid",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Orderid",
                table: "Dishes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Orderid",
                table: "Dishes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_Orderid",
                table: "Dishes",
                column: "Orderid");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Orders_Orderid",
                table: "Dishes",
                column: "Orderid",
                principalTable: "Orders",
                principalColumn: "id");
        }
    }
}
