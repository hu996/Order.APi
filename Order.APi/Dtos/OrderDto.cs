using Order.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.APi.Dtos
{
    public class OrderDto
    {
        

        public decimal TotalAmount { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;
        public List<OrderItem> OrderItems { get; set; }

       
        public int ProductId { get; set; }  
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]

        public Customer customer { get; set; }
    }
}
