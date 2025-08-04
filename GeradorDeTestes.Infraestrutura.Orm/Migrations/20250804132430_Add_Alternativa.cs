using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeradorDeTestes.Infraestrutura.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_Alternativa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternativa_Questoes_QuestaoId",
                table: "Alternativa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alternativa",
                table: "Alternativa");

            migrationBuilder.RenameTable(
                name: "Alternativa",
                newName: "Alternativas");

            migrationBuilder.RenameIndex(
                name: "IX_Alternativa_QuestaoId",
                table: "Alternativas",
                newName: "IX_Alternativas_QuestaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alternativas",
                table: "Alternativas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativas_Questoes_QuestaoId",
                table: "Alternativas",
                column: "QuestaoId",
                principalTable: "Questoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternativas_Questoes_QuestaoId",
                table: "Alternativas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alternativas",
                table: "Alternativas");

            migrationBuilder.RenameTable(
                name: "Alternativas",
                newName: "Alternativa");

            migrationBuilder.RenameIndex(
                name: "IX_Alternativas_QuestaoId",
                table: "Alternativa",
                newName: "IX_Alternativa_QuestaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alternativa",
                table: "Alternativa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativa_Questoes_QuestaoId",
                table: "Alternativa",
                column: "QuestaoId",
                principalTable: "Questoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
