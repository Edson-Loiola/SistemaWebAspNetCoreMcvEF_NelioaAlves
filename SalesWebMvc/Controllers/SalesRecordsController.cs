using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller 
    {

        //para usar a função da classe SalesRecordService que tem a função FindByDate, fazer a injeção de dependencia
        public readonly SalesRecordService _salesRecordService;
        public SalesRecordsController(SalesRecordService salesRecordService) //ctor
        {
            _salesRecordService = salesRecordService;
        }




        public IActionResult Index()
        {
            return View();
        }

        //ação que faz a busca das vendas SimpleSearch
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            //função que vai mostrar o campo data preenchidoo com uma data já
            if (!minDate.HasValue) //se o min data não estiver valor
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1); //setar o 1/01/do anao atual
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyy-MM-dd");



            //chamar aqui aqui a função FindByDate criada na Classe service
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);

            return View(result); //passanda para a view esse resultado
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }

    }
}
