using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class setIsModeratorToAdminAndModeratorToTrue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("59dc4c83-cf09-48da-a0df-6e07187b910b"),
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "IsModerator", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e531c63-306b-44af-b976-7f0b19507c8b", new DateTime(2024, 5, 25, 13, 44, 21, 11, DateTimeKind.Utc).AddTicks(2140), true, "AQAAAAIAAYagAAAAEFWTY9T+l4jZxIqv1pxOpMLJl5f6nfTY4Kyd3sq4gCnV4GzpJJJE8dG0W7/84RiDnw==", "b8eddd32-95b4-4b6d-99f7-e52c43df4e0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9627e039-320e-445b-a8cb-c941d9eb9fba"),
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "IsModerator", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62d42e6e-d523-4eb2-b207-5073e55c0005", new DateTime(2024, 5, 25, 13, 44, 20, 966, DateTimeKind.Utc).AddTicks(3404), true, "AQAAAAIAAYagAAAAECNUJXECyCkxoRZVwLxbcgc8kEXUadn1LF4/Aop8KxkhNctogOpHT4PMwNUv2DtH0w==", "d7d369a9-19a2-4d80-b134-f6eb701d29a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8a00b6b-63ab-4393-8cdb-b7cab31c2726"),
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee986eba-908d-42bd-a351-b5930a0e7691", new DateTime(2024, 5, 25, 13, 44, 21, 56, DateTimeKind.Utc).AddTicks(5914), "AQAAAAIAAYagAAAAEPzwcN1Ho/7hoNIE5IqTCa8TAOoFrbrpo/6KDM7JmWSz0nivLqPYygce/kqADbHE0A==", "dc296607-4901-463d-9de7-4f3365fbf75e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("59dc4c83-cf09-48da-a0df-6e07187b910b"),
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "IsModerator", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e190f9be-8f93-4e1e-8cdb-56787bc2156b", new DateTime(2024, 5, 21, 19, 19, 13, 706, DateTimeKind.Utc).AddTicks(8780), false, "AQAAAAIAAYagAAAAEDC7y9i3lJ3Vvv4Tld4BAQqV3c8G3VvcW4ZjtTJkYllzMGyCS5Z2SnPWdFvOYZGVEw==", "f3ebe92f-78fa-4da7-8de5-ccaaf01f6c12" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9627e039-320e-445b-a8cb-c941d9eb9fba"),
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "IsModerator", "PasswordHash", "SecurityStamp" },
                values: new object[] { "700581f8-65dd-4c63-824f-1178eccd4510", new DateTime(2024, 5, 21, 19, 19, 13, 661, DateTimeKind.Utc).AddTicks(7154), false, "AQAAAAIAAYagAAAAEIXHPaYOQ/mMSF3ef5lv6qwsseBWh2e0dyu7ebymYmtLEPgjkmcUVEPPgXIjNvMkwQ==", "c0536b45-9213-486d-bada-531e89aa1b3c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8a00b6b-63ab-4393-8cdb-b7cab31c2726"),
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97ffe045-e47d-4c0e-b545-cd44ac36923a", new DateTime(2024, 5, 21, 19, 19, 13, 752, DateTimeKind.Utc).AddTicks(2247), "AQAAAAIAAYagAAAAENstLoU+TUppNLNR0QlExkR5GVu7NqV+Y0yx24osK3U/O4msTW2GvKtsVWFkja381g==", "140fd26f-e58a-4805-99e7-f74646b8b6d7" });
        }
    }
}
