using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMVC.Services;

namespace SalesMVC.Controllers
{
    public class SellersController : Controller
    {
        //criando dependencia do seller service
        private readonly SellerService _sellerservice;
        public SellersController(SellerService sellerService)
        {
            _sellerservice = sellerService;
        }
        public IActionResult Index()
        {
            var list = _sellerservice.FindAll();

            return View(list);
        }
    }
}
