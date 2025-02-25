using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Booking_App.Migrations
{
    /// <inheritdoc />
    public partial class bookingroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRooms_Bookings_BookingId",
                table: "BookingRooms");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 24, 11, 9, 41, 710, DateTimeKind.Utc).AddTicks(9752), new byte[] { 214, 226, 113, 129, 149, 103, 16, 35, 235, 139, 180, 183, 38, 217, 10, 175, 72, 160, 62, 141, 75, 9, 27, 246, 88, 254, 215, 134, 244, 191, 91, 196, 67, 142, 252, 83, 93, 111, 103, 39, 141, 43, 112, 108, 84, 230, 142, 84, 204, 128, 127, 143, 44, 11, 168, 141, 170, 228, 149, 231, 33, 118, 213, 203 }, new byte[] { 60, 248, 234, 77, 121, 241, 90, 6, 62, 154, 57, 208, 232, 99, 134, 72, 78, 236, 154, 119, 86, 130, 37, 206, 173, 163, 161, 20, 88, 118, 29, 38, 2, 11, 104, 241, 126, 205, 255, 244, 186, 2, 111, 42, 71, 249, 10, 77, 238, 116, 60, 44, 149, 248, 8, 237, 137, 99, 32, 217, 33, 114, 242, 56, 235, 14, 168, 227, 109, 70, 232, 237, 117, 203, 10, 168, 246, 253, 27, 226, 136, 184, 189, 155, 81, 187, 78, 203, 162, 83, 199, 101, 199, 110, 3, 92, 76, 50, 28, 55, 58, 125, 3, 37, 197, 110, 51, 95, 150, 42, 230, 101, 121, 161, 10, 205, 1, 169, 29, 148, 52, 38, 188, 87, 206, 91, 26, 208 }, new DateTime(2025, 2, 24, 11, 9, 41, 710, DateTimeKind.Utc).AddTicks(9753) });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRooms_Bookings_BookingId",
                table: "BookingRooms",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRooms_Bookings_BookingId",
                table: "BookingRooms");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 24, 10, 35, 29, 889, DateTimeKind.Utc).AddTicks(3424), new byte[] { 237, 41, 119, 162, 227, 47, 147, 248, 18, 221, 195, 107, 211, 55, 83, 21, 153, 19, 24, 70, 237, 110, 114, 140, 87, 48, 104, 111, 132, 100, 164, 70, 140, 182, 166, 1, 255, 69, 178, 99, 3, 186, 46, 151, 184, 92, 68, 6, 122, 146, 223, 132, 57, 190, 219, 159, 158, 80, 26, 19, 188, 218, 89, 226 }, new byte[] { 0, 247, 253, 75, 15, 228, 82, 28, 25, 236, 20, 67, 173, 37, 199, 216, 182, 186, 40, 121, 164, 31, 39, 214, 54, 156, 155, 85, 3, 172, 242, 82, 109, 70, 37, 150, 76, 32, 195, 47, 68, 220, 249, 184, 216, 164, 255, 226, 161, 87, 222, 219, 95, 155, 255, 249, 56, 157, 24, 17, 156, 97, 128, 145, 46, 183, 22, 58, 174, 194, 240, 189, 175, 159, 205, 210, 140, 117, 0, 162, 179, 38, 46, 202, 117, 180, 250, 172, 20, 180, 208, 65, 226, 149, 209, 121, 216, 106, 123, 92, 30, 236, 129, 245, 194, 153, 141, 139, 131, 38, 200, 205, 150, 132, 223, 153, 30, 172, 39, 239, 113, 142, 160, 217, 190, 21, 247, 211 }, new DateTime(2025, 2, 24, 10, 35, 29, 889, DateTimeKind.Utc).AddTicks(3424) });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRooms_Bookings_BookingId",
                table: "BookingRooms",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
