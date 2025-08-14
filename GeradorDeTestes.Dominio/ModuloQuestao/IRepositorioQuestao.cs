using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Dominio.ModuloQuestao;
public interface IRepositorioQuestao : IRepositorio<Questao>
{
    List<Questao> SelecionarPorDisciplinaESerie(Guid disciplinaId, SerieMateria serie, int quantidadeQuestoes);
    List<Questao> SelecionarPorMateria(Guid materiaId, int quantidadeQuestoes);
}