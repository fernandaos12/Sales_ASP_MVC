using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SalesMVC.Services;
using SalesMVC.Models;
using SalesMVC.Models.ViewModels;
using SalesMVC.Services.Exceptions;

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
        {
            _sellerservice.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)//interrogacao opcional perguntar se quer remover
        {
            if (id == null)
            {
                return NotFound();

            }
            var obj = _sellerservice.FindbyId(id.Value);
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
            _sellerservice.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var obj = _sellerservice.FindbyId(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerservice.FindbyId(id.Value);
            if (obj == null)
            {
                return NotFound();
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
            if(id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _sellerservice.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }

    }
}
