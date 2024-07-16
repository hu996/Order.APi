using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; } 
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty; 
        public ICollection<Orders> Orders { get; set; }  



    }
}
