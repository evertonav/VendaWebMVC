using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendaWebMVC.Models.ViewModels
{
    public class VendedorFormViewModel
    {
        public Vendedor Vendedor { get; set; }
        public ICollection<Departamento> Departamentos { get; set; }
    }
}
