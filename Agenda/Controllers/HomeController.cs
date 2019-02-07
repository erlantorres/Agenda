using Agenda.Models;
using Agenda.Models.DTO;
using Agenda.Regra;
using Agenda.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace Agenda.Controllers
{
    public class HomeController : Controller
    {
        private AgendaService _agendaService = new AgendaService();
        public HomeController()
        {
            ViewBag.AgendaService = _agendaService;
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

        [HttpPost]
        public IActionResult AlterarContato(Contato p_Contato)
        {
            RetornoTO retorno;

            if (ModelState.IsValid)
            {
                retorno = _agendaService.AlterarContato(p_Contato);

                if (retorno.Sucesso)
                {
                    return RedirectToAction("DetalheContato", new { p_IdContato = p_Contato.Identificador });
                }
            }

            return RedirectToAction("Error");
        }

        public IActionResult IncluirContato()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IncluirContato(Contato p_Contato)
        {
            RetornoTO retorno;

            if (ModelState.IsValid)
            {
                retorno = _agendaService.IncluirContato(p_Contato);
                if (retorno.Sucesso)
                {
                    return RedirectToAction("Index");

                }
            }

            return RedirectToAction("Error");
        }
        
        public IActionResult ExcluirContato(int p_IdContato)
        {
            RetornoTO ret = _agendaService.ExcluirContato(p_IdContato);
            if (ret.Sucesso)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Error");
        }

        //public JsonResult DadosAdicionais(string p_Tipo, string p_Classificacao, string p_Valor) {


        //    return 
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
