using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(d => d.Name).ToListAsync();

            //ToListAsync é do framework.
            /*Operação sincrona: minha aplicação fica bloqueada e esperando essa operação terminar para depois continuar executando.
            Então em operações mais lentas de acesso a dados disco ou rede nós fazemos operações assíncronas de modo que essa operação executa separadamente 
            e a aplicação continua disponível*/


        }


        //Essa classe DepartmentService presica ser registrado no Startup.cs, no sistema de injeção de dependencia no metodo ConfigureService
    }
}
