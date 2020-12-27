using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }

        //ICollection coleção mais generica que o list ou dictionary
        public ICollection<Department> Departments { get; set; }
    }
}
