using Order.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Entities
{
    public class Orders
    {

        [Key]
        public int OrderId { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public decimal TotalAmount { get;set; }
       
        public string PaymentMethod { get; set; }  =string.Empty;

        public string Status { get; set; } = string.Empty;
        public List<Product> Products { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]

       public Customer customer {  get; set; }    
    }
}
