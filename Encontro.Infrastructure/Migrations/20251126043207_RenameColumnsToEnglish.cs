using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encontro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Pessoas",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Pessoas",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Observacao",
                table: "Pessoas",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Nucleo",
                table: "Pessoas",
                newName: "Group");

            migrationBuilder.RenameColumn(
                name: "NomePai",
                table: "Pessoas",
                newName: "MotherName");

            migrationBuilder.RenameColumn(
                name: "NomeMae",
                table: "Pessoas",
                newName: "FatherName");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Pessoas",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Nascimento",
                table: "Pessoas",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "FotoUrl",
                table: "Pessoas",
                newName: "PhotoUrl");

            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "Pessoas",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Celular",
                table: "Pessoas",
                newName: "CellPhone");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "Pessoas",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Pessoas",
                newName: "District");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Pessoas",
                newName: "CEP");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Pessoas",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Pessoas",
                newName: "FotoUrl");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Pessoas",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Pessoas",
                newName: "Observacao");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Pessoas",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "MotherName",
                table: "Pessoas",
                newName: "NomePai");

            migrationBuilder.RenameColumn(
                name: "Group",
                table: "Pessoas",
                newName: "Nucleo");

            migrationBuilder.RenameColumn(
                name: "FatherName",
                table: "Pessoas",
                newName: "NomeMae");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Pessoas",
                newName: "Bairro");

            migrationBuilder.RenameColumn(
                name: "CellPhone",
                table: "Pessoas",
                newName: "Celular");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Pessoas",
                newName: "Nascimento");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Pessoas",
                newName: "Endereco");
        }
    }
}
