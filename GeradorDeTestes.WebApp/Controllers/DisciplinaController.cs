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
}
