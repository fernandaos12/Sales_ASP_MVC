using System;
using System.Collections.Generic;
using System.Linq;
using SalesMVC.Data;
using SalesMVC.Models;
using Microsoft.EntityFrameworkCore;
using SalesMVC.Services.Exceptions;

namespace SalesMVC.Services
{
    public class SellerService
    {
        private readonly SalesMVCContext _context;

        public SellerService(SalesMVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()  // buscar na base de dados
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            //     obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindbyId(int id)// returns seller with this id or if not exist return null
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        { //se existe algum any dado no bd
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Sorry,Seller Id not found!");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }catch(DbUpdateConcurrencyException e){
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
