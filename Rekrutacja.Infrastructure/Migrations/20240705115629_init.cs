using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rekrutacja.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SubcategoryId = table.Column<int>(type: "int", nullable: true),
                    CustomSubcategory = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contact_Subcategory_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategory",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Służbowy" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Prywatny" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Inny" });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "CategoryId", "CustomSubcategory", "DateOfBirth", "Email", "Name", "Password", "PhoneNumber", "SubcategoryId", "Surname" },
                values: new object[,]
                {
                    { 2, 3, "domowy", new DateTime(1980, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "gabnowa@gmail.com", "Gabriel", "L+0a39rxi2Jo/3DpmVa3GQ==;X7hX4aBVhc+7sioY9wXk2ypssIjvhltlQuTaupJSbPs=", "485216948", null, "Nowak" },
                    { 3, 2, "", new DateTime(2000, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "zancia123@gmail.com", "Żaneta", "2FizU1vwmTqce9CzMPBM9w==;HiorraGCAfyQfF15LvIFVsIerFqw0kjVk8L4IdD5gWg=", "871594862", null, "Pewna" }
                });

            migrationBuilder.InsertData(
                table: "Subcategory",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Szef" },
                    { 2, 1, "Klient" },
                    { 3, 1, "Pracownik" }
                });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "CategoryId", "CustomSubcategory", "DateOfBirth", "Email", "Name", "Password", "PhoneNumber", "SubcategoryId", "Surname" },
                values: new object[] { 1, 1, "", new DateTime(1997, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "adamKowal@gmail.com", "Adam", "ypVYDhynIwMsT8lVkV1tyg==;BHHVobjaCMOQ99zejjnt8NyefwVq7F4wNd4MU/uaTBE=", "+48123321123", 1, "Kowalski" });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CategoryId",
                table: "Contact",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Email",
                table: "Contact",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_SubcategoryId",
                table: "Contact",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategory_CategoryId",
                table: "Subcategory",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Subcategory");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
