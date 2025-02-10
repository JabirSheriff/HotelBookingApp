using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Booking_App.Migrations
{
    /// <inheritdoc />
    public partial class bookingsupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId2",
                table: "BookingRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 10, 12, 42, 13, 963, DateTimeKind.Utc).AddTicks(9147), new byte[] { 103, 77, 41, 129, 51, 18, 24, 246, 203, 80, 21, 236, 189, 140, 56, 174, 91, 224, 24, 233, 94, 140, 16, 139, 12, 166, 63, 249, 69, 156, 195, 64, 141, 98, 236, 195, 193, 217, 228, 148, 81, 233, 178, 151, 191, 9, 245, 167, 119, 248, 2, 237, 228, 78, 240, 67, 16, 20, 44, 115, 227, 213, 207, 38 }, new byte[] { 123, 97, 124, 149, 221, 231, 46, 16, 212, 0, 123, 217, 246, 217, 230, 216, 228, 197, 112, 123, 59, 109, 150, 12, 101, 236, 218, 135, 250, 236, 133, 1, 41, 225, 139, 78, 193, 39, 136, 177, 78, 156, 173, 12, 210, 161, 154, 117, 12, 73, 253, 124, 210, 165, 240, 159, 58, 77, 200, 85, 178, 94, 199, 247, 6, 10, 132, 208, 177, 219, 160, 245, 198, 121, 240, 34, 225, 48, 134, 205, 220, 194, 220, 73, 102, 179, 130, 114, 40, 88, 178, 94, 64, 193, 245, 10, 228, 177, 213, 124, 78, 81, 42, 100, 233, 189, 227, 40, 149, 67, 225, 250, 178, 197, 95, 6, 177, 203, 104, 240, 111, 143, 121, 105, 205, 117, 188, 200 }, new DateTime(2025, 2, 10, 12, 42, 13, 963, DateTimeKind.Utc).AddTicks(9148) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId2",
                table: "BookingRooms");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 10, 12, 26, 28, 844, DateTimeKind.Utc).AddTicks(4216), new byte[] { 224, 38, 186, 221, 69, 155, 72, 56, 92, 123, 252, 105, 109, 138, 35, 179, 32, 86, 75, 111, 69, 235, 87, 180, 144, 126, 13, 252, 87, 17, 43, 23, 27, 146, 10, 77, 77, 118, 34, 81, 109, 121, 145, 255, 183, 253, 215, 154, 200, 140, 204, 44, 167, 222, 21, 212, 94, 97, 39, 50, 56, 192, 131, 74 }, new byte[] { 20, 217, 194, 143, 141, 155, 254, 29, 38, 209, 244, 53, 228, 40, 211, 160, 139, 152, 146, 45, 170, 76, 244, 194, 132, 227, 179, 135, 152, 15, 174, 125, 97, 81, 189, 243, 165, 101, 36, 169, 195, 157, 250, 167, 49, 142, 207, 233, 114, 180, 164, 41, 50, 187, 27, 181, 23, 31, 238, 66, 197, 79, 6, 120, 104, 88, 68, 238, 115, 156, 56, 44, 65, 88, 55, 103, 43, 111, 124, 253, 180, 158, 167, 14, 219, 111, 237, 230, 70, 33, 58, 217, 25, 167, 174, 33, 65, 198, 215, 165, 53, 172, 117, 83, 17, 49, 126, 116, 85, 166, 4, 64, 9, 2, 85, 40, 243, 158, 233, 41, 200, 138, 184, 85, 173, 204, 173, 0 }, new DateTime(2025, 2, 10, 12, 26, 28, 844, DateTimeKind.Utc).AddTicks(4216) });
        }
    }
}
