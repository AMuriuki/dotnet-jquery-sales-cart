using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sales_invoicing_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddProductConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Customers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaxRate = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductConfiguration", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProductConfiguration",
                columns: new[] { "Id", "TaxRate" },
                values: new object[] { 1, 16 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductConfiguration");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Customers");
        }
    }
}
