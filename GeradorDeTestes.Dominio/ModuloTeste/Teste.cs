using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Dominio.ModuloTeste;
public class Teste : EntidadeBase<Teste>
{
    public DateTime DataGeracao { get; set; }
    public string Titulo { get; set; }
    public bool Recuperacao { get; set; }
    public int QteQuestoes { get; set; }
    public Disciplina Disciplina { get; set; }
    public Materia? Materia { get; set; }
    public SerieMateria Serie { get; set; }
    public List<Questao> Questoes { get; set; } = new List<Questao>();

    public Teste() { }

    public Teste(string titulo,
        bool recuperacao, 
        int qteQuestoes,
        SerieMateria serie,
        Disciplina disciplina,
        Materia materia
        ) : this()
    {
        Id = Guid.NewGuid();
        DataGeracao = DateTime.UtcNow;
        Titulo = titulo;    
        Recuperacao = recuperacao;
        Serie = serie;
        Disciplina = disciplina;
        Materia = materia;
    }

    public Teste(string titulo,
    bool recuperacao,
    int qteQuestoes,
    SerieMateria serie,
    Disciplina disciplina,
    Materia materia,
    List<Questao> questoes
    ) : this()
    {
        Id = Guid.NewGuid();
        DataGeracao = DateTime.UtcNow;
        Titulo = titulo;
        Recuperacao = recuperacao;
        Serie = serie;
        Disciplina = disciplina;
        Materia = materia;
        foreach (var item in questoes)
        {
            AdicionarQuestao(item);
        }
    }
    public void AdicionarQuestao(Questao questao)
    {
        questao.UtilizadaEmTeste = true;
        Questoes.Add(questao);
    }

    public void RemoverQuestao(Questao questao)
    {
        questao.UtilizadaEmTeste = false;
        Questoes.Remove(questao);
    }

    public void RemoverQuestoesAtuais()
    {
        for (int i = 0; i < Questoes.Count; i++)
        {
            var questao = Questoes[i];
            RemoverQuestao(questao);
        }
    }

    public List<Questao>? SortearQuestao()
    {
        RemoverQuestoesAtuais();

        var questoesSorteadas = new List<Questao>(QteQuestoes);
        if (Recuperacao)
            questoesSorteadas = Disciplina.SortearQuestoes(QteQuestoes, Serie);
        else
            questoesSorteadas = Materia?.SortearQuestoes(QteQuestoes);

        if(questoesSorteadas is not null)
        {
            foreach (Questao q in questoesSorteadas)
                AdicionarQuestao(q);
        }
        return questoesSorteadas;
    }


    public override void AtualizarRegistro(Teste registroAtualizado)
    {
        Titulo = registroAtualizado.Titulo;
        Disciplina = registroAtualizado.Disciplina;
        Materia = registroAtualizado.Materia;
        Questoes = registroAtualizado.Questoes;
        Recuperacao = registroAtualizado.Recuperacao;
    }
}
