using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Dominio.ModuloMateria;
public class Materia : EntidadeBase<Materia>
{
    public string Nome {  get; set; }
    public SerieMateria Serie {  get; set; }
    public Disciplina Disciplina { get; set; }
    public List<Questao> Questoes { get; set; }
    public List<Teste> Testes { get; set; }

    protected Materia()
    {
        Questoes = new List<Questao>();
        Testes = new List<Teste>();
    }

    public Materia(string nome, SerieMateria serie, Disciplina disciplina) : this() 
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Serie = serie;
        Disciplina = disciplina;
    }

    public override void AtualizarRegistro(Materia registroAtualizado)
    {
        Nome = registroAtualizado.Nome;
        Serie = registroAtualizado.Serie;
        Disciplina = registroAtualizado.Disciplina;
    }

    public void AdicionarQuestoes(List<Questao> questoes)
    {
        if (Questoes.Any(questoes.Contains))
            return;

        Questoes.AddRange(questoes);
    }


    public void AddQuestao(Questao questao)
    {
        if (Questoes.Contains(questao)) return;

        Questoes.Add(questao);
    }

    public void RemoverQuestao(Questao questao)
    {
        if (!Questoes.Contains(questao)) return;

        Questoes.Remove(questao);
    }

    public List<Questao> SortearQuestoes(int qteQuestoes)
    {
        var rand = new Random();

        return Questoes.OrderBy(q => rand.Next()).Take(qteQuestoes).ToList();
    }
}
