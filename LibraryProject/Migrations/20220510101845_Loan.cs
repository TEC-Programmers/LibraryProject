using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.Migrations
{
    public partial class Loan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "loan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    bookId = table.Column<int>(type: "int", nullable: false),
                    loaned_At = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    return_date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "loan",
                columns: new[] { "Id", "bookId", "loaned_At", "return_date", "userID" },
                values: new object[] { 1, 2, "06/05/22", "13/05/22", 2 });

            migrationBuilder.InsertData(
                table: "loan",
                columns: new[] { "Id", "bookId", "loaned_At", "return_date", "userID" },
                values: new object[] { 3, 5, "27/06/22", "27/07/22", 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loan");
        }
    }
}
