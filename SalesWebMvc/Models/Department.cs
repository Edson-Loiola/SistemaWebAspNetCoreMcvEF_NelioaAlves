using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); //um departamento possui muitos vendedores.
        //ICollection é um tipo de coleção mais generica

        public Department()
        { 
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
            //não incluído atributo para a coleção
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            //filtrar todos os vendedores e somar suas vendas
            //passar um sum com a função do total de vendas de cada vendedor e as datas inicial e final
            return Sellers.Sum(sl => sl.TotalSales(initial, final));
        }


    }
}
