using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encontro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CellPhone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    District = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Group = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FatherName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    MotherName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    PhotoUrl = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CellPhone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    District = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    FatherName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Group = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    MotherName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    PhotoUrl = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });
        }
    }
}
