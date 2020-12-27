using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        //declando uma dependencia para SellerService
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;

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
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
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

        //Tela para confirmação
        public IActionResult Delete(int? id) //? Indica que é opcional
        {
            //Caso é feita uma busca indevida(tipo digitando diretamente na url
            if(id == null)
            {
                //Se ele entrar nesse if a aplicação é cortada automaticamente por conta do return
                //Dessa forma o else não é obrigatório
                return NotFound();
            }
            //Por id ser opcional é necessario o Value
           var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
