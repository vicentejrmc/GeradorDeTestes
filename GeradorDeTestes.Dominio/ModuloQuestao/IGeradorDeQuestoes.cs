using GeradorDeTestes.Dominio.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Dominio.ModuloQuestao;
public interface IGeradorDeQuestoes
{
    public Task<List<Questao>> GeradorDeQuestoesAsync(Materia materia, int quantidade);
}
