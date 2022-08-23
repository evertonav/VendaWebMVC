using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendaWebMVC.Servicos;

namespace VendaWebMVC.Controllers
{
    public class RegistroVendasController : Controller
    {
        private readonly RegistroVendasServico _registroVendasServico;

        public RegistroVendasController(RegistroVendasServico registroVendasServico)
        {
            _registroVendasServico = registroVendasServico;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BuscaSimples(DateTime? dataInicial, DateTime? dataFinal)
        {
            if (!dataInicial.HasValue)
            {
                dataInicial = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!dataFinal.HasValue)
            {
                dataFinal = DateTime.Now;
            }

            ViewData["dataInicial"] = dataInicial.Value.ToString("yyyy-MM-dd");
            ViewData["dataFinal"] = dataFinal.Value.ToString("yyyy-MM-dd");

            var listaRegistroVendas = await _registroVendasServico.BuscarPelaDataAsync(dataInicial, dataFinal);
            return View(listaRegistroVendas);
        }

        public async Task<IActionResult> BuscaAgrupada(DateTime? dataInicial, DateTime? dataFinal)
        {
            if (!dataInicial.HasValue)
            {
                dataInicial = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!dataFinal.HasValue)
            {
                dataFinal = DateTime.Now;
            }

            ViewData["dataInicial"] = dataInicial.Value.ToString("yyyy-MM-dd");
            ViewData["dataFinal"] = dataFinal.Value.ToString("yyyy-MM-dd");

            var listaRegistroVendas = await _registroVendasServico.BuscarPelaDataAgrupadoAsync(dataInicial, dataFinal);
            return View(listaRegistroVendas);
        }
    }
}
