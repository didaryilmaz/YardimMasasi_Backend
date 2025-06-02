using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardimMasasi.Migrations
{
    /// <inheritdoc />
    public partial class FixSupportCategoryRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportCategories_Categories_CategoryId",
                table: "SupportCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportCategories_Users_UserId",
                table: "SupportCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "SupportCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupportCategories_CategoryId1",
                table: "SupportCategories",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportCategories_Categories_CategoryId",
                table: "SupportCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportCategories_Categories_CategoryId1",
                table: "SupportCategories",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportCategories_Users_UserId",
                table: "SupportCategories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportCategories_Categories_CategoryId",
                table: "SupportCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportCategories_Categories_CategoryId1",
                table: "SupportCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportCategories_Users_UserId",
                table: "SupportCategories");

            migrationBuilder.DropIndex(
                name: "IX_SupportCategories_CategoryId1",
                table: "SupportCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "SupportCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportCategories_Categories_CategoryId",
                table: "SupportCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportCategories_Users_UserId",
                table: "SupportCategories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
