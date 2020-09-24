using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Database.Entities
{
    public class expense
    {
        [Key]
        public int ExpenseId { get; set; }
        public string PurposeorReason { get; set; }
        public int ExpenseStatus { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string VoucherID { get; set; }

        public int HotelBills { get; set; }
        public int TravelBills { get; set; }
        public int MealsBills { get; set; }

        public int LandlineBills { get; set; }
        public int TransportBills { get; set; }
        public int MobileBills { get; set; }
        public int Miscellaneous { get; set; }
        public int TotalAmount { get; set; }
        public int UserID { get; set; }

        public DateTime CreatedOn { get; set; }
        public string Comment { get; set; }
        public int ProjectID { get; set; }
    }
}
