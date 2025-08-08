using GeradorDeTestes.Dominio.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Dominio.ModuloQuestao;
public interface IGeradorQuestoes
{
    public Task<List<Questao>> GerarQuestoesAsync(Materia materia, int quantidade);
}
