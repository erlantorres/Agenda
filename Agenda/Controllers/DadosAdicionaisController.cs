using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Models;
using Agenda.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Controllers
{
    public class DadosAdicionaisController : Controller
    {
        private AgendaService _agendaService;
        public DadosAdicionaisController()
        {
            _agendaService = new AgendaService();
        }

        public JsonResult AddDadosAdicionais(string p_Tipo, string p_Classificacao, string p_Valor)
        {
            TipoDadoAdicional tipo = _agendaService.ObterTipoDadoAdcional(p_Tipo);
            ClassificacaoDadoAdicional classificacao = _agendaService.ObterClassificacaoDado(p_Classificacao);
            DadoAdicional dado = new DadoAdicional
            {
                TipoDado = tipo,
                ClassificacaoDado = classificacao,
                Valor = p_Valor
            };

            return Json(dado);
        }
    }
}