using System;
using System.ComponentModel.DataAnnotations;
using SalesWebMVC.Models.Enums;
namespace SalesWebMVC.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0}:dd/MM/yyyy")]
        public DateTime Date { get; set; }

        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0}:F2")]
        public double Amount { get; set; }
        public SaleStatus SaleStatus { get; set; }
        public Seller Seller { get; set; }

        public SalesRecord()
        {
        }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus saleStatus, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            SaleStatus = saleStatus;
            Seller = seller;
        }
    }
}
