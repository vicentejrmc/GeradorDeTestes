using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;
public class RepositorioQuestaoOrm : RepositorioBaseOrm<Questao>
{
    public RepositorioQuestaoOrm(GeradorDeTestesDbContext context) : base(context){}

    public List<Questao> SelecionarPorDisciplinaESerie(Guid disciplinaId, SerieMateria serie, int quantidadeQuestoes)
    {
        return registros
            .Include(q => q.Alternativas)
            .Include(q => q.Materia)
            .ThenInclude(m => m.Disciplina)
            .Where(x => x.Materia.Disciplina.Id.Equals(disciplinaId))
            .Where(x => x.Materia.Serie.Equals(serie))
            .Take(quantidadeQuestoes)
            .ToList();
    }

    public List<Questao> SelecionarPorMateria(Guid materiaId, int quantidadeQuestoes)
    {
        return registros
            .Include(q => q.Alternativas)
            .Include(q => q.Materia)
            .Where(x => x.Materia.Id.Equals(materiaId))
            .Take(quantidadeQuestoes)
            .ToList();
    }

    public override Questao? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(x => x.Alternativas)
            .Include(x => x.Materia)
            .FirstOrDefault(x => x.Id.Equals(idRegistro));
    }

    public override List<Questao> SelecionarRegistros()
    {
        return registros
            .Include(x => x.Alternativas)
            .Include(x => x.Materia)
            .ToList();
    }
}
