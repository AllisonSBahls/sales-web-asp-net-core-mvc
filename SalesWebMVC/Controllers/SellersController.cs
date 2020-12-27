using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

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

        public async Task<IActionResult> Index()
        {
            //Pegando os dados do service
            var list = await _sellerService.FindAllAsync();

            //Encaminhando a list das informações para a view
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        //Indicar que a ação é POST e não GET
        [HttpPost]
        //Previnir que a aplicação sofra ataques XSRF/CSRF
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            //Verificação por parte do servidor para verificar se os campos foram preenchidos, caso a opção de javascript esteja desabilitado
            if (!ModelState.IsValid)
            {
                var departaments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departaments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);

            //Redirecionar
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }

        //Tela para confirmação
        public async Task<IActionResult> Delete(int? id) //? Indica que é opcional
        {
            //Caso é feita uma busca indevida(tipo digitando diretamente na url
            if (id == null)
            {
                //Se ele entrar nesse if a aplicação é cortada automaticamente por conta do return
                //Dessa forma o else não é obrigatório
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            //Por id ser opcional é necessario o Value
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
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id) //? Indica que é opcional
        {
            //Caso é feita uma busca indevida(tipo digitando diretamente na url
            if (id == null)
            {
                //Se ele entrar nesse if a aplicação é cortada automaticamente por conta do return
                //Dessa forma o else não é obrigatório
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            //Por id ser opcional é necessario o Value
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        //Abrir o formulario de edição
        public async Task<IActionResult> Edit(int? id)
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
            //Carregar a lista de departamento para povoar a Combobox
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departaments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departaments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
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
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //Pegando o id interno da requisição
            };
            return View(viewModel);
        }
    }
}
