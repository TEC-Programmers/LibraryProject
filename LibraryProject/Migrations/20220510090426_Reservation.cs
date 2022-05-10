using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.API.Migrations
{
    public partial class Reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    bookId = table.Column<int>(type: "int", nullable: false),
                    reserved_At = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reserved_To = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "reservation",
                columns: new[] { "Id", "bookId", "reserved_At", "reserved_To", "userId" },
                values: new object[] { 1, 1, "06/05/22", "13/05/22", 1 });

            migrationBuilder.InsertData(
                table: "reservation",
                columns: new[] { "Id", "bookId", "reserved_At", "reserved_To", "userId" },
                values: new object[] { 2, 2, "14/05/22", "21/05/22", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservation");
        }
    }
}
