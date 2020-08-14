using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        //criar o construtor para que injeção de depência do bd posssa ocorrer
        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //metodo que retorna todos os departamentos
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(d => d.Name).ToList();
        }


        //Essa classe DepartmentService presica ser registrado no Startup.cs, no sistema de injeção de dependencia no metodo ConfigureService
    }
}
