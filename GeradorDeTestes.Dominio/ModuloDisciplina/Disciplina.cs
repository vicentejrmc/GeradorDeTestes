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

    public Disciplina(String nome) : this()
    {
        Guid Id = Guid.NewGuid();
        Nome = nome;
    }
    public List<Questao> SortearQuestoes(int quantidadeQuestoes, SerieMateria serie)
    {
        var questoes = new List<Questao>();

        foreach (var mat in Materias)
        {
            if (mat.Serie.Equals(serie))
                questoes.AddRange(mat.Questoes);
        }

        var rand = new Random();

        return questoes.OrderBy(q => rand.Next()).Take(quantidadeQuestoes).ToList();
    }

    public override void AtualizarRegistro(Disciplina registroEditado)
    {
        Nome = registroEditado.Nome;
    }
}
