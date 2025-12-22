using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encontro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeamNamesToCorrectList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update team names to match the correct list
            migrationBuilder.Sql(@"
                UPDATE Teams SET Name = 'Conselho Arquidiocesano' WHERE ""Order"" = '01a';
                UPDATE Teams SET Name = 'Equipe Dirigente' WHERE ""Order"" = '01b';
                UPDATE Teams SET Name = 'Comandantes Gerais' WHERE ""Order"" = '02';
                UPDATE Teams SET Name = 'Comandantes Jovens' WHERE ""Order"" = '03';
                UPDATE Teams SET Name = 'Espiritualização' WHERE ""Order"" = '04';
                UPDATE Teams SET Name = 'Animação' WHERE ""Order"" = '05';
                UPDATE Teams SET Name = 'Canto' WHERE ""Order"" = '06';
                UPDATE Teams SET Name = 'Círculo' WHERE ""Order"" = '07';
                UPDATE Teams SET Name = 'Prover' WHERE ""Order"" = '08';
                UPDATE Teams SET Name = 'Cozinha' WHERE ""Order"" = '09';
                UPDATE Teams SET Name = 'Creche' WHERE ""Order"" = '10';
                UPDATE Teams SET Name = 'Estacionamento' WHERE ""Order"" = '11';
                UPDATE Teams SET Name = 'Faxina' WHERE ""Order"" = '12';
                UPDATE Teams SET Name = 'Gráfica' WHERE ""Order"" = '13';
                UPDATE Teams SET Name = 'Lanche' WHERE ""Order"" = '14';
                UPDATE Teams SET Name = 'Liturgia e Vigília' WHERE ""Order"" = '15';
                UPDATE Teams SET Name = 'Minimercado' WHERE ""Order"" = '16';
                UPDATE Teams SET Name = 'Sala' WHERE ""Order"" = '17';
                UPDATE Teams SET Name = 'Vigilia e Motivação' WHERE ""Order"" = '18';
                UPDATE Teams SET Name = 'Vigília Paroquial' WHERE ""Order"" = '19';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Restore original names
            migrationBuilder.Sql(@"
                UPDATE Teams SET Name = 'São Francisco' WHERE ""Order"" = '01a';
                UPDATE Teams SET Name = 'Santa Clara' WHERE ""Order"" = '01b';
                UPDATE Teams SET Name = 'São Mateus' WHERE ""Order"" = '02';
                UPDATE Teams SET Name = 'São Marcos' WHERE ""Order"" = '03';
                UPDATE Teams SET Name = 'São Lucas' WHERE ""Order"" = '04';
                UPDATE Teams SET Name = 'São João' WHERE ""Order"" = '05';
                UPDATE Teams SET Name = 'Trabalho' WHERE ""Order"" = '06';
                UPDATE Teams SET Name = 'São Paulo' WHERE ""Order"" = '07';
                UPDATE Teams SET Name = 'Liturgia' WHERE ""Order"" = '08';
                UPDATE Teams SET Name = 'São Pedro' WHERE ""Order"" = '09';
                UPDATE Teams SET Name = 'Sala' WHERE ""Order"" = '10';
                UPDATE Teams SET Name = 'São Tiago Maior' WHERE ""Order"" = '11';
                UPDATE Teams SET Name = 'São Tiago Menor' WHERE ""Order"" = '12';
                UPDATE Teams SET Name = 'Santo André' WHERE ""Order"" = '13';
                UPDATE Teams SET Name = 'Secretaria' WHERE ""Order"" = '14';
                UPDATE Teams SET Name = 'São Bartolomeu' WHERE ""Order"" = '15';
                UPDATE Teams SET Name = 'São Tomé' WHERE ""Order"" = '16';
                UPDATE Teams SET Name = 'Banca' WHERE ""Order"" = '17';
                UPDATE Teams SET Name = 'Cozinha' WHERE ""Order"" = '18';
                UPDATE Teams SET Name = 'Música' WHERE ""Order"" = '19';
            ");
        }
    }
}
