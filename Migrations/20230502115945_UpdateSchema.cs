using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sales_invoicing_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "imageUrl",
                value: "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/31/2411421/1.jpg?5248");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "imageUrl",
                value: "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/05/7399421/1.jpg?3641");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "imageUrl",
                value: "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/94/1328411/1.jpg?9977");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "imageUrl",
                value: "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/71/283233/1.jpg?8328");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "imageUrl",
                value: "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/01/191691/1.jpg?4986");

            migrationBuilder.CreateIndex(
                name: "IX_Products_sku",
                table: "Products",
                column: "sku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_sku",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "Products");
        }
    }
}
