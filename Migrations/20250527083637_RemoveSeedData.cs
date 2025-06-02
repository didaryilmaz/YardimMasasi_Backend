using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YardimMasasi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityId",
                keyValue: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Yazılım Hatası" },
                    { 2, "Donanım Sorunu" },
                    { 3, "Kullanıcı Erişimi" },
                    { 4, "Ağ Problemleri" }
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "PriorityId", "PriorityLevel", "PriorityName" },
                values: new object[,]
                {
                    { 1, 0, "Düşük" },
                    { 2, 0, "Orta" },
                    { 3, 0, "Yüksek" },
                    { 4, 0, "Acil" }
                });
        }
    }
}
