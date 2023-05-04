using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sales_invoicing_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityToSoldProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SoldProducts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SoldProducts");
        }
    }
}
