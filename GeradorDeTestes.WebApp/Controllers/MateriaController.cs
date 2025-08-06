using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;
public class MateriaController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
