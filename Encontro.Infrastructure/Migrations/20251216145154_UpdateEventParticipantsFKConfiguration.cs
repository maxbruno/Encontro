using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encontro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventParticipantsFKConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Teams_TeamId",
                table: "EventParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Teams_TeamId",
                table: "EventParticipants",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Teams_TeamId",
                table: "EventParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Teams_TeamId",
                table: "EventParticipants",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
