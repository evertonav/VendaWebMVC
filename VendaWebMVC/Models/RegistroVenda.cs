using System;
using System.ComponentModel.DataAnnotations;
using VendaWebMVC.Models.Enums;

namespace VendaWebMVC.Models
{
    public class RegistroVenda
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Quantidade { get; set; }
        public StatusVenda Status { get; set; }
        public Vendedor Vendedor { get; set; }

        public RegistroVenda()
        {

        }

        public RegistroVenda(int id, DateTime data, double quantidade, StatusVenda status, Vendedor vendedor)
        {
            Id = id;
            Data = data;
            Quantidade = quantidade;
            Status = status;
            Vendedor = vendedor;
        }
    }
}
