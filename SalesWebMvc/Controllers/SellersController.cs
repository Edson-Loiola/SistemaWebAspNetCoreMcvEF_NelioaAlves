using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        public readonly SellerService _sellerService;
        public readonly DepartmentService _departmentService;

        //faznedo a injeção de dependência
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
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
            var departments = _departmentService.FindAll(); //chamando aqui e carregando o metodo FindAll
            var viewModel = new SellerFormViewModel { Departments = departments }; //instanciando um SellerFormViewModel e passando a lista de departamentos
            return View(viewModel); //passando o objeto para view, quando a tela de cadastro for chamada já estará populada com os objetos

        }

        //inserir dados no banco
        [HttpPost] //esse método é um post pois está criando/enviando um novo objeto
        [ValidateAntiForgeryToken] //essa notação evita que a aplicação receba ataques CSRF (envio de dados malicioso na autenticação)
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); //ao clicar em criar um novo Seller, direciona para a index

        }


        //ainda não é o delete post
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id); //chamando o metodo remove 
            return RedirectToAction(nameof(Index)); 
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }



    }

}
