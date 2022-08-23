using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VendaWebMVC.Models
{
    public class Vendedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve ser entre {2} e {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Preencha com um e-mail válido!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Data Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} deve ser entre {1} e {2}")]
        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double SalarioBase { get; set; }
        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; } //Esse campo com esse nome cria uma ForeignKey no banco quando roda os comando
                                                //Add-Migration e Update-DataBase
        public ICollection<RegistroVenda> Vendas { get; set; } = new List<RegistroVenda>();

        public Vendedor()
        {

        }

        public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salarioBase, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void AdicionarVendas(RegistroVenda registroVenda)
        {
            Vendas.Add(registroVenda);
        }

        public void RemoverVendas(RegistroVenda registroVenda)
        {
            Vendas.Remove(registroVenda);
        }

        public double TotalVendas(DateTime inicial, DateTime final)
        {
            return Vendas
                     .Where(registroVenda => registroVenda.Data >= inicial && registroVenda.Data <= final)
                     .Sum(registroVenda => registroVenda.Quantidade);
        }
    }
}
