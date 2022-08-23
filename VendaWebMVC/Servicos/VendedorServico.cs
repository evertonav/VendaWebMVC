using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendaWebMVC.Data;
using VendaWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using VendaWebMVC.Servicos.Excecoes;

namespace VendaWebMVC.Servicos
{
    public class VendedorServico
    {
        private readonly VendaWebMVCContext _context;

        public VendedorServico(VendaWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Vendedor>> BuscarTodosAsync()
        {
            return await _context.Vendedor.ToListAsync();
        }

        public async Task InserirAsync(Vendedor vendedor)
        {            
            _context.Add(vendedor);
            await _context.SaveChangesAsync();
        }

        public async Task<Vendedor> BuscarPeloIdAsync(int id)
        {
            return await _context.Vendedor.Include(vendedor => vendedor.Departamento).FirstOrDefaultAsync(Vendedor => Vendedor.Id == id);
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var Vendedor = await _context.Vendedor.FindAsync(id);
                _context.Vendedor.Remove(Vendedor);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException excecao)
            {
                throw new IntegrityException(excecao.Message);
            }
        }

        public async Task AtualizarAsync(Vendedor vendedor)
        {
            if (! await _context.Vendedor.AnyAsync(objeto => objeto.Id == vendedor.Id))
            {
                throw new NotFoundException("Id não existente!");
            }

            try
            {
                _context.Update(vendedor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException excecao)
            {
                throw new DbConcurrencyException(excecao.Message);
            }
        }
    }
}
