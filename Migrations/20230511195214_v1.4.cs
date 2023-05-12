using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sales_invoicing_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class v14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductConfiguration",
                table: "ProductConfiguration");

            migrationBuilder.RenameTable(
                name: "ProductConfiguration",
                newName: "ProductConfigurations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductConfigurations",
                table: "ProductConfigurations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductConfigurations",
                table: "ProductConfigurations");

            migrationBuilder.RenameTable(
                name: "ProductConfigurations",
                newName: "ProductConfiguration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductConfiguration",
                table: "ProductConfiguration",
                column: "Id");
        }
    }
}
