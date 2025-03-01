using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_OrderId_To_OrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_Id",
                table: "OrderItem");

            migrationBuilder.EnsureSchema(
                name: "order");

            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products",
                newSchema: "product");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Orders",
                newSchema: "order");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OrderItem",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "order",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderItem");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "user",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "order",
                newName: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OrderItem",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_Id",
                table: "OrderItem",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
