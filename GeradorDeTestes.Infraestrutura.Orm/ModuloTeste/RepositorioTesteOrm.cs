using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;
public class RepositorioTesteOrm : RepositorioBaseOrm<Teste>, IRepositorioTeste
{
    public RepositorioTesteOrm(GeradorDeTestesDbContext context) : base(context){}

    public override Teste? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(t => t.Questoes)
            .ThenInclude(q => q.Alternativas)
            .Include(t => t.Questoes)
            .ThenInclude(q => q.Materia)
            .Include(t => t.Disciplina)
            .Include(t => t.Materia)
            .FirstOrDefault(x => x.Id.Equals(idRegistro));
    }

    public override List<Teste> SelecionarRegistros()
    {
        return registros
            .OrderBy(t => t.Titulo)
            .Include(t => t.Questoes)
            .ThenInclude(q => q.Materia)
            .Include(t => t.Disciplina)
            .Include(t => t.Materia)
            .ToList();
    }
}
