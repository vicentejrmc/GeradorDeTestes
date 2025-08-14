using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GeradorDeTestes.Dominio.ModuloQuestao;
public class Questao : EntidadeBase<Questao>
{
    public string Enunciado { get; set; }
    public bool UtilizadaEmTeste { get; set; }
    public Materia Materia { get; set; }
    public List<Alternativa> Alternativas { get; set; }
    public List<Teste> Testes { get; set; }
    public Alternativa? AlternativaCorreta => Alternativas.Find(a => a.Correta);

    public Questao()
    {
        Alternativas = new List<Alternativa>();
        Testes = new List<Teste>();
    }

    public Questao(string enunciado, Materia materia) : this()
    {
        Id = Guid.NewGuid();
        Enunciado = enunciado;
        Materia = materia;
        UtilizadaEmTeste = false;
    }

    public Alternativa AicionarAlternativa(string resposta, bool correta)
    {
        int qtdAlternativas = Alternativas.Count;

        char letra = (char)('a' + qtdAlternativas);

        var alternativa = new Alternativa(letra, resposta, correta, this);

        Alternativas.Add(alternativa);

        return alternativa;
    }

    public void RemoverAlternativa(char letra)
    {
        if (!Alternativas.Any(a => a.Letra.Equals(letra)))
            return;

        var alternativa = Alternativas.Find(a => a.Letra.Equals(letra));

        if (alternativa == null) return;

        Alternativas.Remove(alternativa);
        ReatribuirLetra();
    }

    private void ReatribuirLetra()
    {
        for (int i = 0; i < Alternativas.Count; i++)
        {
            Alternativas[i].Letra = (char)('a' + i);
        }
    }

    public override void AtualizarRegistro(Questao registroAtualizado)
    {
        Enunciado = registroAtualizado.Enunciado;
        UtilizadaEmTeste = registroAtualizado.UtilizadaEmTeste;
    }
}
