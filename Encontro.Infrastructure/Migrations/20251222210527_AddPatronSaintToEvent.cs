using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encontro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPatronSaintToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatronSaintImageUrl",
                table: "Events",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatronSaintName",
                table: "Events",
                type: "TEXT",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatronSaintImageUrl",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PatronSaintName",
                table: "Events");
        }
    }
}
