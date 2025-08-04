using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
public class RepositorioMateriaOrm : RepositorioBaseOrm<Materia>
{
    public RepositorioMateriaOrm(GeradorDeTestesDbContext context) : base(context){}

    public override Materia? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros.Include(x => x.Disciplina).FirstOrDefault(x => x.Id.Equals(idRegistro));
    }

    public override List<Materia> SelecionarRegistros()
    {
        return registros.Include(x => x.Disciplina).OrderBy(x => x.Nome).ToList();
    }
}
