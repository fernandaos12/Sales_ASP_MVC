using SalesMVC.Data;
using SalesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesMVC.Services
{
    public class SalesRecordService
    {

        private readonly SalesMVCContext _context;

        public SalesRecordService(SalesMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? mindate, DateTime? maxdate)
        {
            var result = from obj in _context.SalesRecords select obj;
            if (mindate.HasValue)
            {
               result = result.Where(x => x.Date >= mindate.Value);
            }
            if (maxdate.HasValue)
            {
                result = result.Where(x => x.Date <= maxdate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
    }
}
