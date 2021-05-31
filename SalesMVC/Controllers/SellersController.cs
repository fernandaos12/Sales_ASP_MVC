using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SalesMVC.Services;
using SalesMVC.Models;
using SalesMVC.Models.ViewModels;
using SalesMVC.Services.Exceptions;
using System.Diagnostics;

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
        public IActionResult Index()
        {
            var list = _sellerservice.FindAll();

            return View(list);
        }

        public IActionResult Create()
        {

            var departments = _departmentservice.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {            //validacao lado servidor para preencher campos
            if (!ModelState.IsValid)
            {
                var departments = _departmentservice.FindAll();
                var viewModel = new SellerFormViewModel { Departments = departments };
                return View(viewModel);
            }
            _sellerservice.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)//interrogacao opcional perguntar se quer remover
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not provided."});

            }
            var obj = _sellerservice.FindbyId(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not found." });
            }
            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerservice.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not Provided." });

            }
            var obj = _sellerservice.FindbyId(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not found." });
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not provided." });
            }
            var obj = _sellerservice.FindbyId(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id not found." });
            }

            // carrega a tela de edicao carregando dados

            List<Department> departments = _departmentservice.FindAll();

            SellerFormViewModel viewmodelseller = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewmodelseller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            //validacao lado servidor para preencher campos
            if (!ModelState.IsValid)
            {
                var departments = _departmentservice.FindAll();
                var viewModel = new SellerFormViewModel { Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
              
                return RedirectToAction(nameof(Error), new { message = "Sorry Seller Id miss match." });
            }
            try
            {
                _sellerservice.Update(seller);
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
