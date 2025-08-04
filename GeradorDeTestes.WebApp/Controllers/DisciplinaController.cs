using GeradorDeTestes.Aplicacao.ModuloDisciplina;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("disciplinas")]
public class DisciplinaController : Controller
{
    private readonly DisciplinaAppService disciplinaAppService;

    public IActionResult Index()
    {
        return View();
    }
}
