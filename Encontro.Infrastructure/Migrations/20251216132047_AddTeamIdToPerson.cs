using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encontro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamIdToPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "People",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_TeamId",
                table: "People",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Teams_TeamId",
                table: "People",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Teams_TeamId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_TeamId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "People");
        }
    }
}
