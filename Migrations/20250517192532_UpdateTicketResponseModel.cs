using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardimMasasi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTicketResponseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "TicketResponses",
                newName: "DateTime");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TicketResponses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketResponses_UserId",
                table: "TicketResponses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketResponses_Users_UserId",
                table: "TicketResponses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketResponses_Users_UserId",
                table: "TicketResponses");

            migrationBuilder.DropIndex(
                name: "IX_TicketResponses_UserId",
                table: "TicketResponses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TicketResponses");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "TicketResponses",
                newName: "dateTime");
        }
    }
}
