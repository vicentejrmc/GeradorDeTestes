using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Dominio.ModuloDisciplina;
public class Disciplina : EntidadeBase<Disciplina>
{
    public string Nome { get; set; }
    public List<Materia> Materias { get; set; }
    public List<Teste> Testes { get; set; }

    protected Disciplina()
    {
        Materias = new List<Materia>();
        Testes = new List<Teste>();
    }

    public Disciplina(string nome) : this()
    {
        Id = Guid.NewGuid();
        Nome = nome;
    }

    public void AdicionarMateria(Materia materia)
    {
        if (Materias.Contains(materia))
            return;

        Materias.Add(materia);
    }

    public List<Questao> SortearQuestoes(int quantidadeQuestoes, SerieMateria serie)
    {
        var questoesRelacionadas = new List<Questao>();

        foreach (var mat in Materias)
        {
            if (mat.Serie.Equals(serie))
                questoesRelacionadas.AddRange(mat.Questoes);
        }

        var random = new Random();

        return questoesRelacionadas
            .OrderBy(q => random.Next())
            .Take(quantidadeQuestoes)
            .ToList();
    }

    public override void AtualizarRegistro(Disciplina registroEditado)
    {
        Nome = registroEditado.Nome;
    }
}
