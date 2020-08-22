using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Excpetions;

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
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync(); //retorna uma lista de Seller

            return View(list); //passando a lista como argumento no método view para gerar um IActionResult contendo a lista

            //...mvc acontecendo aqui: Chamei o controlador, o controlador chamou o model, pegou o dado na lista e encaminha para a view
        }

        public async Task<IActionResult> Create()
        {

            var departments = await _departmentService.FindAllAsync(); //chamando aqui e carregando o metodo FindAll
            var viewModel = new SellerFormViewModel { Departments = departments }; //instanciando um SellerFormViewModel e passando a lista de departamentos
            return View(viewModel); //passando o objeto para view, quando a tela de cadastro for chamada já estará populada com os objetos

        }

        //inserir dados no banco
        [HttpPost] //esse método é um post pois está criando/enviando um novo objeto
        [ValidateAntiForgeryToken] //essa notação evita que a aplicação receba ataques CSRF (envio de dados malicioso na autenticação)
        public async Task<IActionResult> Create(Seller seller)
        {

            //essa validação ocorrerá se o JavaScript do usuário estiver desabilitado, pois não fará as validações feitas no html e nas propriedades
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync(); //carrega os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index)); //ao clicar em criar um novo Seller, direciona para a index

        }


        //ainda não é o delete post
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id); //chamando o metodo remove 
                return RedirectToAction(nameof(Index));

            }
            catch (IntregityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }


        //ação para abrir tele edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) //validação se o id é nulo
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provide" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null) // valida se o obj no banco é nulo
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);

        }

        //ação edit -metodo post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {

            //essa validação ocorrerá se o JavaScript do usuário estiver desabilitado, pois não fará as validações feitas no html e nas propriedades
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync(); //carrega os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }


            if (id != seller.Id) //verifica se o Id é diferente
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismath" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            //alterado esses dois cath por apenas 1 passando a super classe ApplicationException

        }

        public IActionResult Error(string message)
        {
            //ação error
            //objeto view model
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //macete do framework para pegar o id interno da requisão
            };

            return View(viewModel); //retornar o view modo como argumento

            //Error: aqui não tem acesso a dados então não precisa ser assincrono

        }


    }

}
