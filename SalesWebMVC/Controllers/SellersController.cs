using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        //declando uma dependencia para SellerService
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            //Pegando os dados do service
            var list = _sellerService.FindAll();
            
            //Encaminhando a list das informações para a view
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        //Indicar que a ação é POST e não GET
        [HttpPost]
        //Previnir que a aplicação sofra ataques XSRF/CSRF
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);

            //Redirecionar
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }
    }
}
