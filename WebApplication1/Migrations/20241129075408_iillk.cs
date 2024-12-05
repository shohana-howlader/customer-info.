using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class iillk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_DeliveryInfos_DeliveryInfoId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DeliveryInfoId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeliveryInfoId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "DeliveryInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryInfos_CustomerId",
                table: "DeliveryInfos",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryInfos_Customers_CustomerId",
                table: "DeliveryInfos",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryInfos_Customers_CustomerId",
                table: "DeliveryInfos");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryInfos_CustomerId",
                table: "DeliveryInfos");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "DeliveryInfos");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryInfoId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DeliveryInfoId",
                table: "Customers",
                column: "DeliveryInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_DeliveryInfos_DeliveryInfoId",
                table: "Customers",
                column: "DeliveryInfoId",
                principalTable: "DeliveryInfos",
                principalColumn: "DeliveryInfoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
