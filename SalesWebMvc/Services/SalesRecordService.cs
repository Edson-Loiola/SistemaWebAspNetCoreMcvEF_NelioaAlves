using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; //para o Include() no return da dunção FindByDate

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {

        private readonly SalesWebMvcContext _context; //minha classe de contexto do BD


        //criar o construtor para com a injeção de depência do BD
        public SalesRecordService(SalesWebMvcContext contex)
        {
            _context = contex;
        }


        //função que recebe duas datas e retornar as vendas nesse intervalo
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //cria um objeto do tipo SalesRecord e guarda na variavel um objeto do tipo IQueryable que permite fazer outras consultas
            var result = from obj in _context.SalesRecord select obj;

            
            if (minDate.HasValue) //se minDate tem valor, fazer a consulta abaixo
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            else
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return  await result
                .Include(x => x.Seller) //isso faz o join das tabelas  Seller e SalesRecord
                .Include(x => x.Seller.Department) // isso faz o join das tabelas  Sales e Departamento
                .OrderByDescending(x => x.Date) //oredena por data
                .ToListAsync();
        }


    }
}
