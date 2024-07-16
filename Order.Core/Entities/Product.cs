using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }=string.Empty;
        public decimal Price { get; set; }
        public string Stock { get; set; }=string.Empty;
    }
}
