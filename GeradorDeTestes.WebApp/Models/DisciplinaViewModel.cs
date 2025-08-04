using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace GeradorDeTestes.WebApp.Models;

public abstract class FormularioDisciplinaViewModel
{
    [Required(ErrorMessage = "O campo \"Nome\" é brigatório.")]
    [MinLength(3, ErrorMessage = "O campo \"Nome\" precisa ter no minimo 5 caracteres")]
    [MaxLength(100, ErrorMessage = "O campo \"Nome\" pode ter o maximo de 100 caracteres")]
    public string? Nome { get; set; }

    public static Disciplina ParaEntidade(FormularioDisciplinaViewModel viewModel)
    {
        return new Disciplina(viewModel.Nome ?? string.Empty);
    }
}

public class VisualizarDisciplinasViewModel
{
    public List<DetalhesDisciplinaViewModel> Registros { get; set; }

    public VisualizarDisciplinasViewModel(List<Disciplina> disciplinas)
    {
        Registros = new List<DetalhesDisciplinaViewModel>();

        foreach (var item in disciplinas)
        {
            var detalhesVM = DetalhesDisciplinaViewModel.ParaDetalhesVm(item);
            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesDisciplinaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public List<string> Materias { get; set; }

    public DetalhesDisciplinaViewModel(Guid id, string nome, List<Materia> materias)
    {
        Id = id;
        Nome = nome;
        Materias = materias.Select(m => m.Nome).ToList();   
    }

    public static DetalhesDisciplinaViewModel ParaDetalhesVm(Disciplina disciplina)
    {
        return new DetalhesDisciplinaViewModel(disciplina.Id, disciplina.Nome, disciplina.Materias);
    }
}

public class CadastrarDisciplinaViewModel : FormularioDisciplinaViewModel
{
    public CadastrarDisciplinaViewModel() { }

    public CadastrarDisciplinaViewModel(string nome) : this()
    {
        Nome = nome;
    }
}

public class EditarDisciplinaViewModel : FormularioDisciplinaViewModel
{
    public Guid Id { get; set; }

    public EditarDisciplinaViewModel() { }

    public EditarDisciplinaViewModel(Guid id, string nome) : this()
    {
        Id = id;
        Nome = nome;
    }
}

public class ExcluirDisciplinaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public ExcluirDisciplinaViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}