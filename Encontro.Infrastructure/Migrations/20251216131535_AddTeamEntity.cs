using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encontro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Order = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            // Seed initial teams
            var now = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            migrationBuilder.Sql($@"
                INSERT INTO Teams (Id, 'Order', Name, CreatedAt, UpdatedAt) VALUES
                ('{Guid.NewGuid()}', '00', 'Seguimista', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '01a', 'São Francisco', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '01b', 'Santa Clara', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '02', 'São Mateus', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '03', 'São Marcos', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '04', 'São Lucas', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '05', 'São João', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '06', 'Trabalho', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '07', 'São Paulo', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '08', 'Liturgia', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '09', 'São Pedro', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '10', 'Sala', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '11', 'São Tiago Maior', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '12', 'São Tiago Menor', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '13', 'Santo André', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '14', 'Secretaria', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '15', 'São Bartolomeu', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '16', 'São Tomé', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '17', 'Banca', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '18', 'Cozinha', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '19', 'Música', '{now}', '{now}'),
                ('{Guid.NewGuid()}', '20', 'Visitação', '{now}', '{now}')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
