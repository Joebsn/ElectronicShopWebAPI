using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ElectronicShop.Migrations
{
    public partial class ShopMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllExceptionsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "varchar(500)", nullable: true),
                    Level = table.Column<string>(type: "varchar(500)", nullable: true),
                    Logger = table.Column<string>(type: "varchar(500)", nullable: true),
                    Application = table.Column<string>(type: "varchar(500)", nullable: true),
                    Callsite = table.Column<string>(type: "varchar(500)", nullable: true),
                    Exception = table.Column<string>(type: "varchar(500)", nullable: true),
                    Logged = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllExceptionsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "electronicproducts",
                columns: table => new
                {
                    electronicproductID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: true),
                    type = table.Column<string>(type: "varchar(255)", nullable: true),
                    processor = table.Column<string>(type: "varchar(255)", nullable: true),
                    numberofcores = table.Column<int>(type: "int4", nullable: false),
                    screensize = table.Column<int>(type: "int4", nullable: false),
                    memory = table.Column<int>(type: "int4", nullable: false),
                    storage = table.Column<int>(type: "int4", nullable: false),
                    battery = table.Column<int>(type: "int4", nullable: false),
                    numberofproducts = table.Column<int>(type: "int4", nullable: false),
                    price = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_electronicproducts", x => x.electronicproductID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "varchar(255)", nullable: true),
                    lastname = table.Column<string>(type: "varchar(255)", nullable: true),
                    age = table.Column<int>(type: "int4", nullable: false),
                    password = table.Column<string>(type: "varchar(255)", nullable: true),
                    phonenumber = table.Column<int>(type: "int4", nullable: false),
                    balance = table.Column<double>(type: "float8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userID = table.Column<int>(type: "integer", nullable: false),
                    totalnumberofobjectsbought = table.Column<int>(type: "int4", nullable: false),
                    totalprice = table.Column<double>(type: "float8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_orders_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ordersdetails",
                columns: table => new
                {
                    orderdetailID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderID = table.Column<int>(type: "integer", nullable: false),
                    electronicproductID = table.Column<int>(type: "integer", nullable: false),
                    boughtdate = table.Column<DateTime>(type: "Date", nullable: false),
                    quantity = table.Column<int>(type: "int4", nullable: false),
                    price = table.Column<double>(type: "float8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordersdetails", x => x.orderdetailID);
                    table.ForeignKey(
                        name: "FK_ordersdetails_electronicproducts_electronicproductID",
                        column: x => x.electronicproductID,
                        principalTable: "electronicproducts",
                        principalColumn: "electronicproductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordersdetails_orders_orderID",
                        column: x => x.orderID,
                        principalTable: "orders",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "electronicproducts",
                columns: new[] { "electronicproductID", "battery", "memory", "name", "numberofcores", "numberofproducts", "price", "processor", "screensize", "storage", "type" },
                values: new object[,]
                {
                    { 1, 20, 8, "MacbookPro", 8, 1, 1500, "i7", 14, 256, "Laptop" },
                    { 2, 20, 32, "MacbookPro", 8, 1, 1500, "Apple M1", 14, 256, "Laptop" },
                    { 3, 20, 32, "MacbookPro", 8, 2, 750, "i7", 14, 256, "Laptop" },
                    { 4, 20, 8, "MacbookPro", 6, 2, 1500, "i7", 14, 256, "Laptop" },
                    { 5, 20, 8, "MacbookPro", 8, 1, 750, "i7", 14, 256, "Laptop" },
                    { 6, 20, 32, "MacbookPro", 6, 1, 1500, "i7", 14, 256, "Laptop" },
                    { 7, 20, 8, "MacbookPro", 8, 2, 1500, "Apple M1", 14, 256, "Laptop" },
                    { 8, 20, 32, "MacbookPro", 6, 1, 750, "Apple M1", 14, 256, "Laptop" },
                    { 9, 20, 32, "MacbookPro", 6, 2, 1500, "Apple M1", 14, 256, "Laptop" },
                    { 10, 20, 8, "MacbookPro", 6, 1, 750, "i7", 14, 256, "Laptop" },
                    { 11, 20, 32, "MacbookPro", 6, 1, 750, "i7", 14, 256, "Laptop" },
                    { 12, 15, 4, "Iphone12", 4, 3, 800, "A13 bionic", 4, 32, "Phone" },
                    { 13, 15, 4, "Iphone12", 4, 2, 1200, "A15 Bionic", 4, 32, "Phone" },
                    { 14, 15, 8, "Iphone12", 4, 3, 800, "A15 Bionic", 4, 32, "Phone" },
                    { 15, 15, 8, "Iphone12", 4, 1, 1200, "A15 Bionic", 4, 32, "Phone" },
                    { 16, 15, 8, "Iphone12", 4, 2, 1200, "A13 bionic", 4, 32, "Phone" },
                    { 17, 15, 4, "Iphone12", 4, 1, 800, "A15 Bionic", 4, 32, "Phone" },
                    { 18, 15, 4, "Iphone12", 4, 3, 1200, "A13 bionic", 4, 32, "Phone" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "userID", "age", "balance", "firstname", "lastname", "password", "phonenumber" },
                values: new object[,]
                {
                    { 1, 44, 9916.0, "Joe", "Joe", "UqXM", 79795894 },
                    { 2, 20, 18030.0, "John", "John", "gRS", 74795087 },
                    { 3, 19, 18862.0, "Maria", "Maria", "OuUiJhCzo", 71866662 }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "orderID", "totalnumberofobjectsbought", "totalprice", "userID" },
                values: new object[,]
                {
                    { 1, 2, 2100.0, 1 },
                    { 2, 6, 3500.0, 2 },
                    { 3, 1, 2100.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "ordersdetails",
                columns: new[] { "orderdetailID", "boughtdate", "electronicproductID", "orderID", "price", "quantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 2, 13, 14, 34, 50, 221, DateTimeKind.Local).AddTicks(2468), 1, 3, 700.0, 2 },
                    { 2, new DateTime(2022, 2, 13, 14, 34, 50, 221, DateTimeKind.Local).AddTicks(2528), 2, 3, 1166.0, 2 },
                    { 3, new DateTime(2022, 2, 13, 14, 34, 50, 221, DateTimeKind.Local).AddTicks(2546), 1, 1, 833.0, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_userID",
                table: "orders",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_ordersdetails_electronicproductID",
                table: "ordersdetails",
                column: "electronicproductID");

            migrationBuilder.CreateIndex(
                name: "IX_ordersdetails_orderID",
                table: "ordersdetails",
                column: "orderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllExceptionsTable");

            migrationBuilder.DropTable(
                name: "ordersdetails");

            migrationBuilder.DropTable(
                name: "electronicproducts");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
