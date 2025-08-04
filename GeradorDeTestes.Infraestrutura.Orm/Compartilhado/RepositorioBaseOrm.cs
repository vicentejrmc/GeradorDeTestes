using GeradorDeTestes.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
public class RepositorioBaseOrm<T> where T : EntidadeBase<T>
{
    protected readonly DbSet<T> registros;

    public RepositorioBaseOrm(GeradorDeTestesDbContext context)
    {
        this.registros = context.Set<T>();
    }

    public void Cadastrar(T novoRegistro)
    {
        registros.Add(novoRegistro);
    }

    public bool Editar(Guid idRegistro, T registroEditado)
    {
        var registroSelecionado = SelecionarRegistroPorId(idRegistro);

        if (registroSelecionado is null)
            return false;

        registroSelecionado.AtualizarRegistro(registroEditado);

        return true;
    }

    public bool Excluir(Guid idRegistro)
    {
        var registroSelecionado = SelecionarRegistroPorId(idRegistro);

        if (registroSelecionado is null)
            return false;

        registros.Remove(registroSelecionado);

        return true;
    }

    public virtual T? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros.FirstOrDefault(x => x.Id.Equals(idRegistro));
    }

    public virtual List<T> SelecionarRegistros()
    {
        return registros.ToList();
    }

}
