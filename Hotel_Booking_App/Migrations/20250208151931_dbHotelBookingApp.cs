using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Booking_App.Migrations
{
    /// <inheritdoc />
    public partial class dbHotelBookingApp : Migration
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
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Unassigned"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelOwners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PasswordHash", "PasswordSalt", "PhoneNumber", "Role", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 2, 8, 15, 19, 29, 436, DateTimeKind.Utc).AddTicks(6253), "admin@gmail.com", "Admin", new byte[] { 4, 80, 120, 55, 72, 3, 9, 136, 5, 122, 109, 203, 62, 179, 158, 136, 29, 232, 146, 137, 16, 234, 40, 26, 14, 74, 155, 228, 249, 88, 142, 127, 156, 166, 128, 181, 65, 229, 16, 64, 242, 95, 152, 12, 53, 203, 49, 99, 172, 88, 121, 92, 118, 189, 232, 217, 166, 64, 242, 255, 174, 170, 124, 8 }, new byte[] { 204, 225, 181, 186, 74, 193, 140, 16, 174, 216, 191, 189, 183, 199, 25, 6, 137, 133, 161, 201, 166, 111, 30, 148, 80, 221, 239, 129, 202, 180, 189, 48, 109, 215, 226, 173, 229, 246, 205, 15, 99, 5, 233, 73, 110, 159, 100, 175, 167, 121, 35, 64, 20, 212, 206, 127, 198, 247, 234, 212, 144, 152, 126, 34, 105, 152, 219, 161, 209, 224, 71, 138, 172, 222, 43, 26, 16, 124, 203, 9, 17, 184, 25, 70, 248, 69, 193, 72, 135, 85, 164, 125, 255, 151, 103, 155, 151, 247, 86, 195, 254, 146, 154, 216, 207, 69, 3, 251, 154, 184, 164, 10, 157, 54, 153, 91, 29, 46, 226, 255, 124, 92, 152, 171, 102, 22, 254, 217 }, "7397388965", "Admin", new DateTime(2025, 2, 8, 15, 19, 29, 436, DateTimeKind.Utc).AddTicks(6253) });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelOwners_UserId",
                table: "HotelOwners",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "HotelOwners");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
