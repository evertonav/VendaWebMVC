using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VendaWebMVC.Data;
using VendaWebMVC.Models;
using VendaWebMVC.Models.ViewModels;
using VendaWebMVC.Servicos;
using VendaWebMVC.Servicos.Excecoes;

namespace VendaWebMVC.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorServico _vendedorServico;
        private readonly DepartamentosServico _departamentoServico;

        public async Task<IActionResult> Index()
        {
            var list = await _vendedorServico.BuscarTodosAsync();
            return View(list);
        }

        public VendedoresController(VendedorServico vendedorServico, DepartamentosServico departamentosServico)
        {
            _vendedorServico = vendedorServico;
            _departamentoServico = departamentosServico;
        }

        public async Task<IActionResult> Create()
        {
            var departamentos = await _departamentoServico.BuscarTodosAsync();
            var vendedorViewModel = new VendedorFormViewModel() { Departamentos = departamentos };
            return View(vendedorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Previnir ataque CSRF
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                List<Departamento> departamentos = await _departamentoServico.BuscarTodosAsync();
                VendedorFormViewModel vendedorFormViewModel = new VendedorFormViewModel()
                {
                    Departamentos = departamentos,
                    Vendedor = vendedor
                };

                return View(vendedorFormViewModel);
            }

            await _vendedorServico.InserirAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido!" });
            }

            var vendedor = await _vendedorServico.BuscarPeloIdAsync(id.Value);
            
            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não existe!" });
            }

            return View(vendedor);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vendedorServico.RemoverAsync(id);
                return RedirectToAction(nameof(Index));
            } 
            catch (IntegrityException excecao)
            {
                return RedirectToAction(nameof(Error), new { Message = "Não posso deletar o vendedor por que ele(a) tem vendas" });
            }
        }

        public async Task<IActionResult> Detalhe(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido!" });
            }

            var vendedor = await _vendedorServico.BuscarPeloIdAsync(id.Value);

            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não existe!" });
            }

            return View(vendedor);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido!" });
            }

            var vendedor = await _vendedorServico.BuscarPeloIdAsync(id.Value);

            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não existe!" });
            }

            List<Departamento> departamentos = await _departamentoServico.BuscarTodosAsync();
            VendedorFormViewModel vendedorFormViewModel = new VendedorFormViewModel() 
                                                                { Vendedor = vendedor, Departamentos = departamentos };

            return View(vendedorFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                List<Departamento> departamentos = await _departamentoServico.BuscarTodosAsync();
                VendedorFormViewModel vendedorFormViewModel = new VendedorFormViewModel()
                {
                    Departamentos = departamentos,
                    Vendedor = vendedor
                };

                return View(vendedorFormViewModel);
            }

            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não corresponde!" }); 
            }

            try
            {
                await _vendedorServico.AtualizarAsync(vendedor);
                return RedirectToAction(nameof(Index));
            } 
            catch (ApplicationException exception)
            {
                return RedirectToAction(nameof(Error), new { Message = exception.Message });
            }
        }

        public IActionResult Error(string message)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel()
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(errorViewModel);
        }
    }
}
