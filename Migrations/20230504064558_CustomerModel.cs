using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sales_invoicing_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class CustomerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fname = table.Column<string>(type: "TEXT", nullable: true),
                    lname = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "fname", "lname" },
                values: new object[,]
                {
                    { 1, "Wanjiku", "Mwangi" },
                    { 2, "Kipchoge", "Keino" },
                    { 3, "Auma", "Obama" },
                    { 4, "Mumbi", "Ngugi" },
                    { 5, "Omondi", "Odinga" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
