using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeradorDeTestes.Infraestrutura.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_Questao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternativa_Questao_QuestaoId",
                table: "Alternativa");

            migrationBuilder.DropForeignKey(
                name: "FK_Questao_Materias_MateriaId",
                table: "Questao");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestaoTeste_Questao_QuestoesId",
                table: "QuestaoTeste");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questao",
                table: "Questao");

            migrationBuilder.RenameTable(
                name: "Questao",
                newName: "Questoes");

            migrationBuilder.RenameIndex(
                name: "IX_Questao_MateriaId",
                table: "Questoes",
                newName: "IX_Questoes_MateriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questoes",
                table: "Questoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativa_Questoes_QuestaoId",
                table: "Alternativa",
                column: "QuestaoId",
                principalTable: "Questoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestaoTeste_Questoes_QuestoesId",
                table: "QuestaoTeste",
                column: "QuestoesId",
                principalTable: "Questoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questoes_Materias_MateriaId",
                table: "Questoes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternativa_Questoes_QuestaoId",
                table: "Alternativa");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestaoTeste_Questoes_QuestoesId",
                table: "QuestaoTeste");

            migrationBuilder.DropForeignKey(
                name: "FK_Questoes_Materias_MateriaId",
                table: "Questoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questoes",
                table: "Questoes");

            migrationBuilder.RenameTable(
                name: "Questoes",
                newName: "Questao");

            migrationBuilder.RenameIndex(
                name: "IX_Questoes_MateriaId",
                table: "Questao",
                newName: "IX_Questao_MateriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questao",
                table: "Questao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativa_Questao_QuestaoId",
                table: "Alternativa",
                column: "QuestaoId",
                principalTable: "Questao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questao_Materias_MateriaId",
                table: "Questao",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestaoTeste_Questao_QuestoesId",
                table: "QuestaoTeste",
                column: "QuestoesId",
                principalTable: "Questao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
