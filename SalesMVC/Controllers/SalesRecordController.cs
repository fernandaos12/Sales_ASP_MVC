using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMVC.Controllers
{
    public class SalesRecordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleSearch()
        {
            return View();
        }

        public IActionResult GroupSearch()
        {
            return View();
        }
    }
}
