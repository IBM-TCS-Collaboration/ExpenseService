using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Database.Entities
{
    public class expenseAudit
    {
        [Key]
        public int ApprovalExpenseLogId { get; set; }
        public int ApprovalUser { get; set; }
        public DateTime ProcessedDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
        public int ExpenseID { get; set; }
        public int UserID { get; set; }

    }
}
