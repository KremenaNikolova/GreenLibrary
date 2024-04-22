using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("261ef0dc-4c66-4c6d-9d92-0aca4888ed13"), "261EF0DC-4C66-4C6D-9D92-0ACA4888ED13", "Moderator", "MODERATOR" },
                    { new Guid("8ff0dacb-d7db-4286-8361-bf4f49c13802"), "8FF0DACB-D7DB-4286-8361-BF4F49C13802", "User", "USER" },
                    { new Guid("98a1f1a7-e250-473a-8765-49ca43260d6f"), "98A1F1A7-E250-473A-8765-49CA43260D6F", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "Image", "IsDeleted", "IsModerator", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("59dc4c83-cf09-48da-a0df-6e07187b910b"), 0, "6622641d-9252-489e-b1ef-e819683423e1", new DateTime(2024, 4, 22, 9, 33, 35, 646, DateTimeKind.Utc).AddTicks(4227), "moderator@test.com", true, "Елица", null, false, false, "Емилова", false, null, "MODERATOR@TEST.COM", "MODERATOR", "AQAAAAIAAYagAAAAEBJeHL2pNyEudi1OFlegRw9orZZiWG081b2SfaOwAqNk07RGlFTurjSbcRq300qp0A==", null, false, null, false, "moderator" },
                    { new Guid("9627e039-320e-445b-a8cb-c941d9eb9fba"), 0, "f862afe0-208c-4cb3-bc45-cb01514c3f86", new DateTime(2024, 4, 22, 9, 33, 35, 600, DateTimeKind.Utc).AddTicks(8628), "admin@test.com", true, "Георги", null, false, false, "Иванов", false, null, "ADMIN@TEST.COM", "ADMIN", "AQAAAAIAAYagAAAAENgdmAJuBOYKjPmbnduYdLHJtv44qjwTF8V5uBbRnfNoIkZ1Kx2dp5k536/br5iX4w==", null, false, null, false, "admin" },
                    { new Guid("f8a00b6b-63ab-4393-8cdb-b7cab31c2726"), 0, "0444ac29-4cdd-462b-87c1-132da36611ac", new DateTime(2024, 4, 22, 9, 33, 35, 691, DateTimeKind.Utc).AddTicks(2096), "user@test.com", true, "Петър", null, false, false, "Петров", false, null, "USER@TEST.COM", "USER", "AQAAAAIAAYagAAAAEGfEEGU6F2zcBDgh+Y3gUYpHZoJQ6Ho8Vdzrfy6NiqQ+Qb7VUA18yip9SNd214PHsA==", null, false, null, false, "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("261ef0dc-4c66-4c6d-9d92-0aca4888ed13"), new Guid("59dc4c83-cf09-48da-a0df-6e07187b910b") },
                    { new Guid("98a1f1a7-e250-473a-8765-49ca43260d6f"), new Guid("9627e039-320e-445b-a8cb-c941d9eb9fba") },
                    { new Guid("8ff0dacb-d7db-4286-8361-bf4f49c13802"), new Guid("f8a00b6b-63ab-4393-8cdb-b7cab31c2726") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("261ef0dc-4c66-4c6d-9d92-0aca4888ed13"), new Guid("59dc4c83-cf09-48da-a0df-6e07187b910b") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("98a1f1a7-e250-473a-8765-49ca43260d6f"), new Guid("9627e039-320e-445b-a8cb-c941d9eb9fba") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8ff0dacb-d7db-4286-8361-bf4f49c13802"), new Guid("f8a00b6b-63ab-4393-8cdb-b7cab31c2726") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("261ef0dc-4c66-4c6d-9d92-0aca4888ed13"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8ff0dacb-d7db-4286-8361-bf4f49c13802"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("98a1f1a7-e250-473a-8765-49ca43260d6f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("59dc4c83-cf09-48da-a0df-6e07187b910b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9627e039-320e-445b-a8cb-c941d9eb9fba"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8a00b6b-63ab-4393-8cdb-b7cab31c2726"));
        }
    }
}
