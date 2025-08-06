using GeradorDeTestes.Aplicacao.ModuloDisciplina;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("disciplinas")]
public class DisciplinaController : Controller
{
    private readonly DisciplinaAppService disciplinaAppService;

    public DisciplinaController(DisciplinaAppService disciplinaAppService)
    {
        this.disciplinaAppService = disciplinaAppService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var result = disciplinaAppService.SelecionarTodos();

        if(result.IsFailed)
        {
            foreach(var erro in result.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada
                    (erro.Message, erro.Reasons[0].Message);

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson); break;
            }
            return RedirectToAction("erro", "home");
        }

        var registros = result.Value;
        var visualizarVm = new VisualizarDisciplinasViewModel(registros);
        var notificacao = TempData.TryGetValue(nameof(NotificacaoViewModel), out var valor);

        if(notificacao && valor is string jsonString)
        {
            var notificacaoVM = JsonSerializer.Deserialize<NotificacaoViewModel>(jsonString);
            ViewData.Add(nameof(NotificacaoViewModel), notificacaoVM);
        }

        return View(visualizarVm);
    }

    [HttpGet("cadastrar")]   
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarDisciplinaViewModel();
        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarDisciplinaViewModel cadastrarVm)
    {
        var entidade = FormularioDisciplinaViewModel.ParaEntidade(cadastrarVm);
        var result = disciplinaAppService.Cadastrar(entidade);

        if(result.IsFailed)
        {
            foreach(var erro in result.Errors)
            {
                if (erro.Metadata["TipoErro"].ToString() == "RegistroDuplicado")
                    ModelState.AddModelError("CadastroUnico", erro.Reasons[0].Message); break;

            }
            return View(cadastrarVm);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var resultado = disciplinaAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada
                    (erro.Message,erro.Reasons[0].Message);

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson); break;
            }
            return RedirectToAction(nameof(Index));
        }

        var registroSelecionado = resultado.Value;

        var editarVM = new EditarDisciplinaViewModel(id,registroSelecionado.Nome);

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarDisciplinaViewModel editarVM)
    {
        var entidadeEditada = FormularioDisciplinaViewModel.ParaEntidade(editarVM);
        var resultado = disciplinaAppService.Editar(id, entidadeEditada);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                if (erro.Metadata["TipoErro"].ToString() == "RegistroDuplicado")
                    ModelState.AddModelError("CadastroUnico", erro.Reasons[0].Message); break;
            }
            return View(editarVM);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var resultado = disciplinaAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada
                    (erro.Message,erro.Reasons[0].Message);

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson); break;
            }
            return RedirectToAction(nameof(Index));
        }

        var registroSelecionado = resultado.Value;

        var excluirVM = new ExcluirDisciplinaViewModel(registroSelecionado.Id,registroSelecionado.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        var resultado = disciplinaAppService.Excluir(id);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada
                    (erro.Message,erro.Reasons[0].Message);

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson); break;
            }
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var resultado = disciplinaAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada
                    (erro.Message, erro.Reasons[0].Message);

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson); break;
            }
        }
        var detalhesVm = DetalhesDisciplinaViewModel.ParaDetalhesVm(resultado.Value);

        return View(detalhesVm);
    }
}
