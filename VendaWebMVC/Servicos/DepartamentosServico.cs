using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendaWebMVC.Data;
using VendaWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace VendaWebMVC.Servicos
{
    public class DepartamentosServico
    {
        private readonly VendaWebMVCContext _context;

        public DepartamentosServico(VendaWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Departamento>> BuscarTodosAsync()
        {
            return await _context.Departamento.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
