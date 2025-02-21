using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Booking_App.Migrations
{
    /// <inheritdoc />
    public partial class hotelmodelmodify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amenities",
                table: "Hotels");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 20, 9, 18, 25, 278, DateTimeKind.Utc).AddTicks(1858), new byte[] { 90, 130, 135, 229, 205, 79, 118, 147, 146, 111, 74, 140, 99, 197, 218, 112, 201, 49, 230, 66, 193, 155, 110, 101, 165, 51, 84, 163, 193, 252, 29, 182, 166, 200, 191, 97, 60, 170, 238, 190, 116, 29, 230, 143, 211, 109, 231, 101, 121, 112, 71, 217, 212, 173, 23, 152, 77, 43, 197, 230, 157, 159, 7, 204 }, new byte[] { 138, 108, 88, 213, 157, 71, 24, 182, 99, 9, 191, 16, 95, 189, 233, 83, 230, 224, 84, 143, 159, 219, 227, 242, 67, 57, 67, 63, 131, 179, 58, 245, 15, 139, 7, 76, 0, 35, 40, 154, 140, 58, 195, 118, 225, 67, 101, 146, 220, 18, 63, 78, 192, 160, 185, 188, 19, 53, 158, 214, 186, 195, 150, 95, 140, 21, 255, 165, 222, 118, 88, 167, 80, 232, 60, 241, 156, 120, 6, 85, 102, 151, 69, 198, 87, 22, 204, 57, 62, 101, 152, 240, 29, 4, 143, 98, 226, 242, 230, 146, 153, 102, 92, 131, 134, 144, 58, 52, 19, 209, 144, 139, 106, 184, 128, 92, 203, 52, 212, 159, 84, 162, 102, 32, 181, 174, 91, 162 }, new DateTime(2025, 2, 20, 9, 18, 25, 278, DateTimeKind.Utc).AddTicks(1859) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Amenities",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 19, 5, 29, 42, 334, DateTimeKind.Utc).AddTicks(3857), new byte[] { 30, 167, 168, 61, 156, 199, 110, 216, 228, 76, 15, 42, 253, 168, 190, 50, 35, 232, 190, 41, 172, 210, 150, 212, 19, 243, 118, 18, 17, 230, 239, 82, 212, 209, 23, 116, 106, 9, 135, 253, 132, 167, 82, 89, 50, 250, 137, 28, 45, 171, 211, 120, 230, 222, 236, 124, 165, 195, 16, 199, 207, 132, 228, 13 }, new byte[] { 79, 18, 105, 32, 207, 225, 12, 18, 162, 222, 244, 46, 16, 210, 200, 248, 145, 147, 33, 76, 242, 198, 143, 58, 255, 179, 196, 154, 44, 61, 245, 105, 31, 29, 74, 18, 168, 20, 103, 51, 83, 87, 194, 228, 16, 158, 183, 123, 194, 47, 87, 166, 136, 189, 10, 103, 161, 148, 43, 73, 214, 208, 43, 141, 128, 80, 167, 173, 197, 20, 152, 180, 0, 73, 21, 59, 181, 110, 78, 98, 51, 197, 219, 54, 97, 22, 50, 115, 212, 208, 103, 249, 158, 31, 242, 62, 181, 120, 253, 98, 65, 49, 119, 64, 192, 194, 134, 52, 113, 25, 162, 232, 216, 80, 124, 7, 136, 202, 164, 77, 123, 56, 111, 5, 236, 80, 83, 194 }, new DateTime(2025, 2, 19, 5, 29, 42, 334, DateTimeKind.Utc).AddTicks(3857) });
        }
    }
}
