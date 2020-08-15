using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Excpetions;
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
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);

            //eager loading (inlcude): inner join para carregar outros objetos associados ao obj principal (no caso o departamento)
        }


        //função remover vendedor do banco pelo id
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();

        }


        //metofo update
        public void Update(Seller obj)
        {
            //pra atualizar um objeto o id desse objeto já precisa existir no banco
            if (!_context.Seller.Any(x => x.Id == obj.Id)) // verifica se expressão passada não existe no banco
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
            _context.Update(obj); //atualiza o objeto
            _context.SaveChanges(); //confirmar alteração
            }
            catch (DbConcurrencyException e)
            {

                throw new DbConcurrencyException(e.Message);
                //exception de acesso a dados capturadas pelo serviço 
            }
        }



        //Essa classe SalesWebMvcContext presica ser registrado no Startup.cs no sistema de injeção de dependencia no metodo ConfigureService
    }
}
