using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Booking_App.Migrations
{
    /// <inheritdoc />
    public partial class specialrequestnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpecialRequest",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 10, 10, 5, 8, 847, DateTimeKind.Utc).AddTicks(2355), new byte[] { 219, 155, 83, 183, 178, 213, 255, 115, 5, 14, 187, 152, 78, 61, 153, 38, 212, 193, 78, 228, 174, 45, 6, 184, 98, 44, 82, 6, 54, 56, 156, 51, 54, 41, 111, 91, 239, 213, 124, 123, 126, 133, 190, 254, 221, 65, 127, 61, 95, 74, 20, 194, 67, 200, 97, 66, 71, 249, 124, 134, 62, 201, 59, 31 }, new byte[] { 181, 150, 153, 116, 214, 13, 67, 198, 145, 169, 40, 240, 7, 69, 230, 179, 183, 111, 6, 50, 3, 152, 234, 238, 93, 199, 25, 81, 194, 10, 156, 159, 118, 172, 131, 77, 31, 169, 219, 8, 12, 238, 62, 20, 22, 206, 191, 22, 173, 84, 9, 215, 234, 161, 198, 182, 59, 192, 33, 53, 89, 66, 81, 194, 56, 170, 170, 243, 166, 173, 196, 2, 69, 144, 86, 203, 122, 204, 39, 62, 228, 226, 120, 241, 139, 86, 133, 61, 246, 222, 18, 36, 35, 158, 137, 173, 164, 8, 70, 128, 164, 129, 149, 99, 158, 25, 33, 184, 126, 47, 105, 196, 174, 169, 140, 56, 19, 202, 227, 147, 203, 111, 138, 220, 229, 251, 255, 98 }, new DateTime(2025, 2, 10, 10, 5, 8, 847, DateTimeKind.Utc).AddTicks(2356) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpecialRequest",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 9, 19, 38, 59, 818, DateTimeKind.Utc).AddTicks(384), new byte[] { 128, 14, 239, 10, 143, 153, 163, 143, 96, 96, 217, 69, 241, 203, 42, 27, 94, 231, 243, 17, 134, 27, 195, 184, 43, 211, 108, 86, 14, 238, 220, 253, 67, 213, 152, 152, 25, 202, 26, 101, 164, 227, 181, 139, 68, 226, 213, 130, 12, 68, 17, 153, 138, 130, 211, 6, 212, 116, 181, 133, 167, 100, 94, 203 }, new byte[] { 92, 3, 157, 159, 94, 141, 54, 180, 235, 180, 42, 131, 125, 200, 70, 195, 39, 71, 222, 217, 113, 182, 159, 232, 152, 11, 132, 182, 234, 226, 45, 180, 114, 71, 61, 200, 62, 4, 74, 8, 124, 134, 93, 169, 81, 95, 232, 200, 154, 94, 24, 89, 177, 203, 124, 23, 220, 76, 11, 126, 40, 174, 240, 89, 228, 241, 198, 173, 215, 118, 7, 177, 31, 196, 165, 86, 207, 238, 203, 115, 195, 40, 146, 208, 7, 43, 18, 185, 31, 166, 231, 235, 136, 49, 52, 161, 152, 185, 37, 94, 255, 99, 150, 128, 195, 79, 34, 110, 87, 93, 14, 6, 58, 102, 135, 95, 107, 245, 73, 73, 98, 218, 123, 144, 72, 195, 21, 131 }, new DateTime(2025, 2, 9, 19, 38, 59, 818, DateTimeKind.Utc).AddTicks(384) });
        }
    }
}
