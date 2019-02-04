using Agenda.Models;
using Agenda.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Agenda.Controllers
{
    public class HomeController : Controller
    {
        private AgendaService _agendaService;

        public HomeController()
        {
            _agendaService = new AgendaService();
        }

        public IActionResult Index(string p_Filter)
        {
            List<Contato> contatos = _agendaService.ListarContatos(p_Filter);
            return View(contatos);
        }

        public IActionResult DetalheContato(int p_IdContato)
        {
            Contato contato = _agendaService.ObterContatoPorId(p_IdContato);
            return View(contato);
        }

        public IActionResult AlterarContato(int p_IdContato)
        {
            Contato contato = _agendaService.ObterContatoPorId(p_IdContato);
            return View(contato);
        }

        public IActionResult IncluirContato()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
