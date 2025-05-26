using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardimMasasi.Migrations
{
    /// <inheritdoc />
    public partial class FixUserRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AssignedSupportId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId2",
                table: "Tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId1",
                table: "Tickets",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId2",
                table: "Tickets",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AssignedSupportId",
                table: "Tickets",
                column: "AssignedSupportId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId1",
                table: "Tickets",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId2",
                table: "Tickets",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AssignedSupportId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId1",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId2",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UserId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UserId2",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AssignedSupportId",
                table: "Tickets",
                column: "AssignedSupportId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
