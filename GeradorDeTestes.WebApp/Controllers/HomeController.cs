﻿using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GeradorDeTestes.WebApp.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("erro")]
    public IActionResult Erro()
    {
        var existeNotificacao = TempData.TryGetValue(nameof(NotificacaoViewModel), out var valor);

        if (existeNotificacao && valor is string jsonString)
        {
            var notificacaoVm = JsonSerializer.Deserialize<NotificacaoViewModel>(jsonString);

            ViewData.Add(nameof(NotificacaoViewModel), notificacaoVm);
        }

        return View();
    }
}
