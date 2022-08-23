using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VendaWebMVC.Models;

namespace VendaWebMVC.Data
{
    public class VendaWebMVCContext : DbContext
    {
        public VendaWebMVCContext (DbContextOptions<VendaWebMVCContext> options)
            : base(options)
        {
        }

        public DbSet<VendaWebMVC.Models.Departamento> Departamento { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<RegistroVenda> RegistroVendas { get; set; }
    }
}
