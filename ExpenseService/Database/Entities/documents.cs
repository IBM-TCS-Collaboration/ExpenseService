using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Database.Entities
{
    public class documents
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentBytes { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ExpenseId { get; set; }
        public string DocumentType { get; set; }

    }
}
