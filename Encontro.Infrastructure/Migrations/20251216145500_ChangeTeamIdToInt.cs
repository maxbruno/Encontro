using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encontro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTeamIdToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints first
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Teams_TeamId",
                table: "EventParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Teams_TeamId",
                table: "People");

            // Create new Teams table with int ID
            migrationBuilder.CreateTable(
                name: "Teams_New",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Order = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            // Copy data from old Teams table (ordered by Order so IDs are sequential)
            migrationBuilder.Sql(@"
                INSERT INTO Teams_New (""Order"", Name, CreatedAt, UpdatedAt)
                SELECT ""Order"", Name, CreatedAt, UpdatedAt 
                FROM Teams 
                ORDER BY ""Order"";
            ");

            // Drop old Teams table
            migrationBuilder.DropTable(name: "Teams");

            // Rename new table
            migrationBuilder.RenameTable(name: "Teams_New", newName: "Teams");

            // Update EventParticipants to remove TeamId (we'll set it to NULL since we can't map GUIDs to ints)
            migrationBuilder.Sql("UPDATE EventParticipants SET TeamId = NULL;");

            // Update People to remove TeamId
            migrationBuilder.Sql("UPDATE People SET TeamId = NULL;");

            // Alter columns to int
            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "People",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "EventParticipants",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Teams_TeamId",
                table: "EventParticipants",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Teams_TeamId",
                table: "People",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Teams",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamId",
                table: "People",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamId",
                table: "EventParticipants",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
