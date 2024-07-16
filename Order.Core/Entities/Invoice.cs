using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Entities
{
    public class Invoice
    {

        [Key]
        public int InvoiceId { get; set; }
        public int OrderId { get; set;}

        public DateTime InvoiceDate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
