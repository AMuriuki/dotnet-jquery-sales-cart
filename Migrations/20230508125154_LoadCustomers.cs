using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sales_invoicing_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class LoadCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "lname",
                table: "Customers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "fname",
                table: "Customers",
                newName: "LoyaltyCard");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Customers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Customers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Customers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Customers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Customers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Customers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LoyaltyPoints",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LoyaltyPoints",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customers",
                newName: "lname");

            migrationBuilder.RenameColumn(
                name: "LoyaltyCard",
                table: "Customers",
                newName: "fname");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "fname", "lname" },
                values: new object[,]
                {
                    { 1, "Wanjiku", "Mwangi" },
                    { 2, "Kipchoge", "Keino" },
                    { 3, "Auma", "Obama" },
                    { 4, "Mumbi", "Ngugi" },
                    { 5, "Omondi", "Odinga" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "imageUrl" },
                values: new object[,]
                {
                    { 1, "Apple AirPods Pro", 8500.00m, "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/31/2411421/1.jpg?5248" },
                    { 2, "Xiaomi Redmi Buds", 2700.00m, "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/05/7399421/1.jpg?3641" },
                    { 3, "Oraimo Wireless Earphone", 3400.00m, "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/94/1328411/1.jpg?9977" },
                    { 4, "Netac USB Type-C 128 GB", 1600.00m, "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/71/283233/1.jpg?8328" },
                    { 5, "Sandisk USB 32GB ", 8500.00m, "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/01/191691/1.jpg?4986" }
                });
        }
    }
}
