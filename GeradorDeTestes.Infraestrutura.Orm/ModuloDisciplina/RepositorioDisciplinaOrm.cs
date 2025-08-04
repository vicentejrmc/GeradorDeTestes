using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
public class RepositorioDisciplinaOrm : RepositorioBaseOrm<Disciplina>, IRepositorioDisciplina
{
    public RepositorioDisciplinaOrm(GeradorDeTestesDbContext context) : base(context){}

    public override Disciplina? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros.Include(m => m.Materias).FirstOrDefault(m => m.Id.Equals(idRegistro));
    }

    public override List<Disciplina> SelecionarRegistros()
    {
        return registros.Include(m => m.Materias).OrderBy(m => m.Nome).ToList();
    }
}
