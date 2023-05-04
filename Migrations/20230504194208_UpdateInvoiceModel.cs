using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sales_invoicing_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountPercent",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountValue",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GrandTotal",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Shipping",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaxPercent",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaxValue",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "GrandTotal",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TaxPercent",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TaxValue",
                table: "Invoices");
        }
    }
}
