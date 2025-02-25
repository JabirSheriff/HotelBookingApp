using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Booking_App.Migrations
{
    /// <inheritdoc />
    public partial class Bookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomTypeId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "BookingRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "BookingRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 24, 9, 32, 32, 308, DateTimeKind.Utc).AddTicks(4769), new byte[] { 141, 229, 16, 56, 58, 190, 81, 62, 116, 228, 187, 69, 118, 222, 244, 150, 59, 74, 248, 186, 147, 15, 77, 66, 133, 73, 31, 183, 221, 154, 243, 21, 150, 122, 198, 233, 216, 198, 44, 225, 105, 11, 35, 159, 25, 191, 202, 236, 137, 144, 227, 13, 77, 26, 213, 53, 161, 59, 14, 47, 196, 142, 83, 120 }, new byte[] { 121, 2, 34, 154, 9, 79, 179, 182, 218, 215, 8, 223, 108, 154, 210, 136, 177, 250, 168, 233, 90, 140, 82, 136, 81, 65, 235, 83, 224, 73, 107, 50, 91, 238, 130, 158, 232, 13, 69, 73, 226, 99, 128, 89, 66, 30, 124, 143, 89, 95, 120, 168, 145, 233, 120, 167, 72, 20, 194, 59, 14, 16, 58, 244, 239, 118, 23, 189, 133, 157, 146, 127, 101, 28, 147, 137, 172, 207, 30, 90, 85, 50, 192, 236, 163, 250, 67, 12, 155, 29, 73, 110, 20, 136, 112, 242, 142, 73, 201, 203, 46, 44, 131, 227, 41, 103, 194, 132, 212, 154, 61, 20, 198, 122, 13, 160, 83, 88, 8, 182, 115, 254, 253, 140, 79, 103, 129, 63 }, new DateTime(2025, 2, 24, 9, 32, 32, 308, DateTimeKind.Utc).AddTicks(4769) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "BookingRooms");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "BookingRooms");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 20, 9, 18, 25, 278, DateTimeKind.Utc).AddTicks(1858), new byte[] { 90, 130, 135, 229, 205, 79, 118, 147, 146, 111, 74, 140, 99, 197, 218, 112, 201, 49, 230, 66, 193, 155, 110, 101, 165, 51, 84, 163, 193, 252, 29, 182, 166, 200, 191, 97, 60, 170, 238, 190, 116, 29, 230, 143, 211, 109, 231, 101, 121, 112, 71, 217, 212, 173, 23, 152, 77, 43, 197, 230, 157, 159, 7, 204 }, new byte[] { 138, 108, 88, 213, 157, 71, 24, 182, 99, 9, 191, 16, 95, 189, 233, 83, 230, 224, 84, 143, 159, 219, 227, 242, 67, 57, 67, 63, 131, 179, 58, 245, 15, 139, 7, 76, 0, 35, 40, 154, 140, 58, 195, 118, 225, 67, 101, 146, 220, 18, 63, 78, 192, 160, 185, 188, 19, 53, 158, 214, 186, 195, 150, 95, 140, 21, 255, 165, 222, 118, 88, 167, 80, 232, 60, 241, 156, 120, 6, 85, 102, 151, 69, 198, 87, 22, 204, 57, 62, 101, 152, 240, 29, 4, 143, 98, 226, 242, 230, 146, 153, 102, 92, 131, 134, 144, 58, 52, 19, 209, 144, 139, 106, 184, 128, 92, 203, 52, 212, 159, 84, 162, 102, 32, 181, 174, 91, 162 }, new DateTime(2025, 2, 20, 9, 18, 25, 278, DateTimeKind.Utc).AddTicks(1859) });
        }
    }
}
