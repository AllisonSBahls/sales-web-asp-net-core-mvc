using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class Department
    {
        //Basic Attributes
        public int Id { get; set; }
        [Display(Name = "Departamento")]
        public string Name { get; set; }

        //Icolleciton é genecito que aceita list, hash entre outros
        //Association
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        //Constructors
        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //Custom Methods
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        //Total de Vendas do departamento
        public double TotalSales(DateTime initial, DateTime final)
        {
            //Para calcular será necessário pegar a quantidade vendida de cada vendedor e somar todos eles
            //Para isso estou pegando o total no periodo inicial e final do vendedor passando
            //as datas de vendas do departamento, assim para cada um será feito o calculo do vendedor e somado no departamento
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
