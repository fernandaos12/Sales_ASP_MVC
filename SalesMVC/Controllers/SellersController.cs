using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SalesMVC.Services;
using SalesMVC.Models;
using SalesMVC.Models.ViewModels;

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
    }
}
