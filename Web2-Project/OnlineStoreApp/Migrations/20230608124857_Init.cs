using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineStoreApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PositionX = table.Column<double>(type: "float", nullable: false),
                    PositionY = table.Column<double>(type: "float", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 6, 8, 14, 48, 56, 10, DateTimeKind.Local).AddTicks(7880)),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OrderPrice = table.Column<double>(type: "float", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SellerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Birthday", "Email", "FullName", "Image", "Password", "Type", "Username", "VerificationStatus" },
                values: new object[,]
                {
                    { 1, "Admin 123", new DateTime(1978, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@luka.com", "Admin Admin", null, "$2a$11$y3ljyt5D1ueyLUTYwF9kEurUtgwy6xeJQOtUcMuYz/1SdnU9gRfQG", "Administrator", "admin", "Waiting" },
                    { 2, "Seller 123", new DateTime(1978, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "luka.ciric2000@gmail.com", "Seller Seller", null, "$2a$11$t0UdwHpeSSf8MSRvyuWHwOSQxxkYWimS/BjW/Cm91.vXZr4Mmud8u", "Seller", "seller", "Accepted" },
                    { 3, "Buyer 123", new DateTime(1978, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "buyer@luka.com", "Buyer Buyer", null, "$2a$11$RC9NBxPPgOa9257z15IHBu2xu.MLtbln5PsYFQRgXwN/z1pWSc5Ju", "Buyer", "buyer", "Waiting" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Comment", "DeliveryAddress", "DeliveryTime", "OrderPrice", "PositionX", "PositionY", "UserId" },
                values: new object[] { 1, null, "Dr Sime Milosevica 10, Novi Sad", new DateTime(2023, 6, 8, 17, 3, 56, 11, DateTimeKind.Local).AddTicks(4230), 9.5, 45.245005556420359, 19.850283596223083, 3 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amount", "Description", "Image", "Name", "Price", "SellerId" },
                values: new object[] { 1, 10, "123", null, "Grapes", 1.2, 2 });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Amount", "Name", "OrderId", "Price", "ProductId" },
                values: new object[] { 1, 5, "Test", 1, 1.2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Item_OrderId",
                table: "Item",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
