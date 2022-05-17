using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.API.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    PublishYear = table.Column<short>(type: "smallint", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "FirstName", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { 1, "Astrid", " Lindgrens", "" },
                    { 2, "Helle", "Helle", "" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "KidsBook" },
                    { 2, "Roman" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "peter@abc.com", "Peter", "Aksten", "Per.", "password", 0 },
                    { 2, "riz@abc.com", "Rizwanah", "Mustafa", "R.R", "password", 1 }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Description", "Language", "PublishYear", "Title" },
                values: new object[] { 1, 1, 1, "BØg for børn", "Danish", (short)1945, " Pippi Langstrømper" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Description", "Language", "PublishYear", "Title" },
                values: new object[] { 2, 2, 2, "Romaner for voksen2", "Danish", (short)2005, "Rødby-Puttgarden" });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
