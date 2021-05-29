using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMVC.Data;
using SalesMVC.Models;


namespace SalesMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesMVCContext _context;

        public DepartmentService(SalesMVCContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
     }
}
