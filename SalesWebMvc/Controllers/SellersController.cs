using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}
