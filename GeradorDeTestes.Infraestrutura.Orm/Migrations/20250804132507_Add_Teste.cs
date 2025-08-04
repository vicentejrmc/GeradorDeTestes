using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeradorDeTestes.Infraestrutura.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_Teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestaoTeste_Teste_TestesId",
                table: "QuestaoTeste");

            migrationBuilder.DropForeignKey(
                name: "FK_Teste_Disciplinas_DisciplinaId",
                table: "Teste");

            migrationBuilder.DropForeignKey(
                name: "FK_Teste_Materias_MateriaId",
                table: "Teste");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teste",
                table: "Teste");

            migrationBuilder.RenameTable(
                name: "Teste",
                newName: "Testes");

            migrationBuilder.RenameIndex(
                name: "IX_Teste_MateriaId",
                table: "Testes",
                newName: "IX_Testes_MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Teste_DisciplinaId",
                table: "Testes",
                newName: "IX_Testes_DisciplinaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Testes",
                table: "Testes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestaoTeste_Testes_TestesId",
                table: "QuestaoTeste",
                column: "TestesId",
                principalTable: "Testes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Testes_Disciplinas_DisciplinaId",
                table: "Testes",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Testes_Materias_MateriaId",
                table: "Testes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestaoTeste_Testes_TestesId",
                table: "QuestaoTeste");

            migrationBuilder.DropForeignKey(
                name: "FK_Testes_Disciplinas_DisciplinaId",
                table: "Testes");

            migrationBuilder.DropForeignKey(
                name: "FK_Testes_Materias_MateriaId",
                table: "Testes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Testes",
                table: "Testes");

            migrationBuilder.RenameTable(
                name: "Testes",
                newName: "Teste");

            migrationBuilder.RenameIndex(
                name: "IX_Testes_MateriaId",
                table: "Teste",
                newName: "IX_Teste_MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Testes_DisciplinaId",
                table: "Teste",
                newName: "IX_Teste_DisciplinaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teste",
                table: "Teste",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestaoTeste_Teste_TestesId",
                table: "QuestaoTeste",
                column: "TestesId",
                principalTable: "Teste",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teste_Disciplinas_DisciplinaId",
                table: "Teste",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teste_Materias_MateriaId",
                table: "Teste",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id");
        }
    }
}
