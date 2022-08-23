using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendaWebMVC.Data;
using VendaWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace VendaWebMVC.Servicos
{
    public class RegistroVendasServico
    {
        private readonly VendaWebMVCContext _context;

        public RegistroVendasServico(VendaWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<RegistroVenda>> BuscarPelaDataAsync(DateTime? dataInicial, DateTime? dataFinal)
        {
            var resultado = from obj in _context.RegistroVendas select obj;

            if (dataInicial.HasValue)
            {
                resultado = resultado.Where(x => x.Data >= dataInicial);
            }

            if (dataFinal.HasValue)
            {
                resultado = resultado.Where(x => x.Data <= dataFinal);
            }

            return await resultado
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Departamento, RegistroVenda>>> BuscarPelaDataAgrupadoAsync(DateTime? dataInicial, DateTime? dataFinal)
        {
            var resultado = from obj in _context.RegistroVendas select obj;

            if (dataInicial.HasValue)
            {
                resultado = resultado.Where(x => x.Data >= dataInicial);
            }

            if (dataFinal.HasValue)
            {
                resultado = resultado.Where(x => x.Data <= dataFinal);
            }

            return await resultado
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .GroupBy(x => x.Vendedor.Departamento)
                .ToListAsync();
        }

    }
}
