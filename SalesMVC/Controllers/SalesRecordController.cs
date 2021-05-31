using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMVC.Services;

namespace SalesMVC.Controllers
{
    public class SalesRecordController : Controller
    {
        private readonly SalesRecordService _salesrecordservice;

        public SalesRecordController(SalesRecordService salesrecordservice)
        {
            _salesrecordservice = salesrecordservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? mindate, DateTime? maxdate)
        {
            if (!mindate.HasValue)
            {
                mindate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxdate.HasValue)
            {
                maxdate = DateTime.Now;
            }
            ViewData["mindate"] = mindate.Value.ToString("yyyy-MM-dd");
            ViewData["maxdate"] = mindate.Value.ToString("yyyy-MM-dd");

            var result = await _salesrecordservice.FindByDateAsync(mindate, maxdate);
            return View(result);
        }

        public IActionResult GroupSearch()
        {
            return View();
        }
    }
}
