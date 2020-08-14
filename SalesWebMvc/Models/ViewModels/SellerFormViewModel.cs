using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        //Dados necessário para uma tela de cadastro de vendor

        public Seller Seller { get; set; } // objeto do vendedor
        public ICollection<Department> Departments { get; set; } //coleção generica de Departamentos



    }
}
