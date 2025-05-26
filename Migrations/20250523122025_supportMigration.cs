using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardimMasasi.Migrations
{
    /// <inheritdoc />
    public partial class supportMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedSupportId",
                table: "Tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedSupportId",
                table: "Tickets",
                column: "AssignedSupportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AssignedSupportId",
                table: "Tickets",
                column: "AssignedSupportId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AssignedSupportId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AssignedSupportId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AssignedSupportId",
                table: "Tickets");
        }
    }
}
