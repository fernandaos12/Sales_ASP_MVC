using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [StringLength(60, MinimumLength = 3 , ErrorMessage = "{0} Name should be minimun {1} lenght caracteres {2}")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Field Required")]
        public string Email { get; set; }
       
        [Display(Name = "Birth Date")] //customizando a label da table
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Field Required")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Base Salary")]
        [Required(ErrorMessage = "Field Required")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }  //foreignkey para nao ficar nulo
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime dateTime, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            DateTime = dateTime;
            BaseSalary = baseSalary;
            Department = department;
        }


        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime inicial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= inicial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}