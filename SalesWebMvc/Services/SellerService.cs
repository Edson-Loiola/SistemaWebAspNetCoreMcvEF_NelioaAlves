using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        //criar o construtor para que injeção de depência posssa ocorrer
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {

            _context.Add(obj); //função para pegar os dados do formulario e salvar no banco
            _context.SaveChanges(); //função para confirmar a gravação dos dados no banco

        }

        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }


        //função remover vendedor do banco pelo id
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();

        }

        //Essa classe SalesWebMvcContext presica ser registrado no Startup.cs no sistema de injeção de dependencia no metodo ConfigureService
    }
}
