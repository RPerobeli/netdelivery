using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Net.Delivery.Order.Domain.Migrations
{
    /// <inheritdoc />
    public partial class usertype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TIPO_USUARIO",
                table: "USUARIOS",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TIPO_USUARIO",
                table: "USUARIOS");
        }
    }
}
