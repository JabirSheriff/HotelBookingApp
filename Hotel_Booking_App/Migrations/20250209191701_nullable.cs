using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Booking_App.Migrations
{
    /// <inheritdoc />
    public partial class nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StarRating",
                table: "Hotels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Hotels",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhone",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 9, 19, 17, 0, 350, DateTimeKind.Utc).AddTicks(3706), new byte[] { 204, 180, 114, 58, 159, 237, 120, 9, 64, 17, 170, 146, 71, 236, 69, 74, 151, 13, 8, 135, 216, 162, 188, 148, 111, 233, 217, 23, 25, 137, 200, 187, 232, 72, 38, 31, 144, 253, 229, 46, 214, 35, 122, 128, 178, 14, 62, 150, 159, 25, 23, 52, 78, 12, 221, 182, 254, 145, 23, 192, 10, 50, 198, 4 }, new byte[] { 24, 9, 169, 62, 215, 165, 58, 192, 183, 250, 63, 137, 210, 241, 254, 108, 203, 190, 226, 10, 121, 26, 138, 170, 93, 193, 147, 160, 9, 240, 252, 190, 85, 102, 175, 218, 91, 85, 188, 211, 78, 103, 111, 95, 93, 254, 136, 211, 62, 89, 62, 175, 71, 113, 102, 65, 123, 191, 100, 67, 166, 253, 15, 105, 87, 21, 51, 162, 36, 220, 187, 118, 135, 5, 240, 81, 5, 230, 40, 134, 251, 185, 206, 102, 192, 15, 141, 99, 50, 195, 243, 60, 57, 18, 245, 7, 159, 77, 214, 160, 158, 199, 224, 81, 242, 165, 42, 222, 14, 124, 11, 196, 169, 48, 4, 102, 102, 149, 140, 191, 25, 184, 72, 19, 158, 95, 87, 88 }, new DateTime(2025, 2, 9, 19, 17, 0, 350, DateTimeKind.Utc).AddTicks(3707) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StarRating",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhone",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 9, 19, 13, 8, 186, DateTimeKind.Utc).AddTicks(4338), new byte[] { 74, 51, 53, 139, 170, 3, 12, 23, 179, 190, 26, 91, 164, 96, 133, 1, 249, 239, 48, 148, 45, 65, 3, 75, 87, 37, 123, 29, 99, 199, 160, 27, 228, 15, 29, 79, 167, 203, 137, 13, 196, 204, 104, 206, 38, 127, 227, 162, 25, 149, 108, 159, 73, 42, 215, 54, 99, 58, 43, 121, 153, 99, 76, 199 }, new byte[] { 89, 81, 168, 135, 104, 103, 199, 212, 53, 138, 113, 9, 218, 195, 159, 42, 42, 90, 89, 24, 166, 38, 201, 235, 22, 231, 60, 214, 71, 172, 41, 112, 226, 36, 251, 35, 131, 116, 163, 206, 5, 35, 68, 208, 45, 136, 212, 155, 139, 214, 68, 177, 228, 239, 92, 126, 209, 195, 44, 207, 66, 205, 253, 101, 124, 144, 225, 206, 254, 171, 58, 50, 245, 79, 172, 73, 2, 116, 56, 85, 144, 195, 53, 84, 232, 41, 118, 226, 106, 223, 90, 68, 245, 242, 71, 239, 247, 133, 21, 255, 32, 188, 43, 38, 63, 215, 18, 97, 98, 32, 110, 18, 209, 186, 105, 122, 58, 243, 159, 127, 144, 23, 45, 129, 242, 166, 15, 216 }, new DateTime(2025, 2, 9, 19, 13, 8, 186, DateTimeKind.Utc).AddTicks(4339) });
        }
    }
}
