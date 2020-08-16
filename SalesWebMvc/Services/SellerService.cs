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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //public void Insert(Seller obj)
        public async Task InsertAsync(Seller obj)
        {

            _context.Add(obj); //função para pegar os dados do formulario e salvar no banco
            await _context.SaveChangesAsync(); //função para confirmar a gravação dos dados no banco, (aqui deve ter a versão async)

        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);

            //eager loading (inlcude): inner join para carregar outros objetos associados ao obj principal (no caso o departamento)
        }


        //função remover vendedor do banco pelo id
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();

        }


        //metofo update
        public async Task UpdateAsync(Seller obj)
        {
            //pra atualizar um objeto o id desse objeto já precisa existir no banco

            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny) // verifica se expressão passada não existe no banco
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj); //atualiza o objeto
             await _context.SaveChangesAsync(); //confirmar alteração
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
