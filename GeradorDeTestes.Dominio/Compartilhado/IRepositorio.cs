﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Dominio.Compartilhado;
public interface IRepositorio<T> where T : EntidadeBase<T>
{
    public void Cadastrar(T novoRegistro);
    public bool Editar(Guid idRegistro, T registroEditado);
    public bool Excluir(Guid idRegistro);
    public List<T> SelecionarRegistros();
    public T? SelecionarRegistroPorId(Guid idRegistro);
}
