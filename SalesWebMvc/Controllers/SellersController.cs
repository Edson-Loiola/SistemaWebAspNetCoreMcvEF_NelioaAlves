using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        public readonly SellerService _sellerService;

        //faznedo a injeção de dependência
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }


        //implementar a chamada do metodo sellerService
        public IActionResult Index()
        {
            var list = _sellerService.FindAll(); //retorna uma lista de Seller

            return View(list); //passando a lista como argumento no método view para gerar um IActionResult contendo a lista

            //...mvc acontecendo aqui: Chamei o controlador, o controlador chamou o model, pegou o dado na lista e encaminha para a view
        }

        public IActionResult Create()
        {
            return View();

        }

        //inserir dados no banco
        [HttpPost] //esse método é um post pois está criando/enviando um novo objeto
        [ValidateAntiForgeryToken] //essa notação evita que a aplicação receba ataques CSRF (envio de dados malicioso na autenticação)
        public IActionResult Create (Seller seller)
        {
            _sellerService.Insert(seller);            
            return RedirectToAction(nameof(Index)); //ao clicar em criar um novo Seller, direciona para a index
        
        }
    }
}
