using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Net.Delivery.Order.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Pedidos_itensMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITEM_PEDIDOS_OrderId",
                table: "ITEM");

            migrationBuilder.DropColumn(
                name: "ItemIdList",
                table: "PEDIDOS");

            migrationBuilder.CreateTable(
                name: "PEDIDO_ITENS",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_ITENS", x => new { x.OrderId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_PEDIDO_ITENS_ITEM_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ITEM",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PEDIDO_ITENS_PEDIDOS_OrderId",
                        column: x => x.OrderId,
                        principalTable: "PEDIDOS",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_ITENS_ItemId",
                table: "PEDIDO_ITENS",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ITEM_PEDIDOS_OrderId",
                table: "ITEM",
                column: "OrderId",
                principalTable: "PEDIDOS",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITEM_PEDIDOS_OrderId",
                table: "ITEM");

            migrationBuilder.DropTable(
                name: "PEDIDO_ITENS");

            migrationBuilder.AddColumn<string>(
                name: "ItemIdList",
                table: "PEDIDOS",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ITEM_PEDIDOS_OrderId",
                table: "ITEM",
                column: "OrderId",
                principalTable: "PEDIDOS",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
