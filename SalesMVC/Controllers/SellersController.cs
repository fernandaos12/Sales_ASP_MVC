using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SalesMVC.Services;
using SalesMVC.Models;
using SalesMVC.Models.ViewModels;
using SalesMVC.Services.Exceptions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SalesMVC.Controllers
{
    public class SellersController : Controller
    {
        //criando dependencia do seller service
        private readonly SellerService _sellerservice;
        private readonly DepartmentService _departmentservice;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerservice = sellerService;
            _departmentservice = departmentService;
        }
        public async Task <IActionResult> Index()
        {
            var list = await _sellerservice.FindAllAsync();

            return View(list);
        }

        public async Task<IActionResult> Create()
        {

            var departments = await _departmentservice.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {            //validacao lado servidor para preencher campos
            if (!ModelState.IsValid)
            {
                var departments = await _departmentservice.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments };
                return View(viewModel);
            }
           await _sellerservice.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)//interrogacao opcional perguntar se quer remover
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not provided."});

            }
            var obj = await _sellerservice.FindbyIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not found." });
            }
            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerservice.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not Provided." });

            }
            var obj = await _sellerservice.FindbyIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not found." });
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not provided." });
            }
            var obj = await _sellerservice.FindbyIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not found." });
            }

            // carrega a tela de edicao carregando dados

            List<Department> departments = await _departmentservice.FindAllAsync();

            SellerFormViewModel viewmodelseller = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewmodelseller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            //validacao lado servidor para preencher campos
            if (!ModelState.IsValid)
            {
                var departments = await _departmentservice.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
              
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id miss match." });
            }
            try
            {
                await _sellerservice.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewmodel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
            return View(viewmodel);

        }

    }
}
