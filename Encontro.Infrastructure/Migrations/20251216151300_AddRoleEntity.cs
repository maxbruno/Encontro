using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encontro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "EventParticipants",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
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
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            // Seed 21 roles
            var now = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            migrationBuilder.Sql($@"
                INSERT INTO Roles (""Order"", Name, CreatedAt, UpdatedAt) VALUES
                ('01', 'Diretor Espiritual', '{now}', '{now}'),
                ('02', 'Casal Montagem', '{now}', '{now}'),
                ('03', 'Casal Fichas', '{now}', '{now}'),
                ('04', 'Casal Finanças', '{now}', '{now}'),
                ('05', 'Casal Palestras', '{now}', '{now}'),
                ('06', 'Casal Pós Encontro', '{now}', '{now}'),
                ('07', 'Jovem Montagem', '{now}', '{now}'),
                ('08', 'Jovem Fichas', '{now}', '{now}'),
                ('09', 'Jovem Finanças', '{now}', '{now}'),
                ('10', 'Jovem Palestras', '{now}', '{now}'),
                ('11', 'Jovem Pós Encontro', '{now}', '{now}'),
                ('12', 'Coordenador(a)', '{now}', '{now}'),
                ('13', 'Membro', '{now}', '{now}'),
                ('14', 'Chefinho(a)', '{now}', '{now}'),
                ('15', 'Piloto(a)', '{now}', '{now}'),
                ('16', 'Chefão(ona)', '{now}', '{now}'),
                ('17', 'Tio(a)', '{now}', '{now}'),
                ('18', 'Círculo Amarelo', '{now}', '{now}'),
                ('19', 'Círculo Azul', '{now}', '{now}'),
                ('20', 'Círculo Vermelho', '{now}', '{now}'),
                ('21', 'Círculo Verde', '{now}', '{now}'),
                ('22', 'Círculo Laranja', '{now}', '{now}')
            ");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_RoleId",
                table: "EventParticipants",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Roles_RoleId",
                table: "EventParticipants",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Roles_RoleId",
                table: "EventParticipants");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_EventParticipants_RoleId",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "EventParticipants");
        }
    }
}
