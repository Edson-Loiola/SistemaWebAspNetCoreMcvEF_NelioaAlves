using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; } //um vendedor possui um departamento
        public int DepartmentId { get; set; } //essa prop vai garantir que id do departamneto não seja nulo quando cadastrar um Seller
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); //um vendedor possui muitas vendas
        //ICollection é um tipo de coleção mais generica


        public Seller()
        { 
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
            //não incluído atributo para a coleção
        }

        public void AddSales(SalesRecord sr) //esse metodo recebe como argumento um SalesRecord sr
        {
            Sales.Add(sr); // e adiciona na minha lista o sr
        }

        public void RemoveSales(SalesRecord sr) //esse metodo recebe como argumento um SalesRecord sr
        {
            Sales.Remove(sr); // e revome da minha lista o sr
        }


        public double TotalSales(DateTime initial, DateTime final)
        {
            //Para essa operação usar LINQ
            //fazer um filtro (where) e obter  uma lista no intervalo de data recebido no parametro
            //em outra expressão lambda chamar o metodo soma passando o atributo Amount da classe SalesRecord
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);

        }

    }
}
