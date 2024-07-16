using System.ComponentModel.DataAnnotations;

namespace Order.APi.Dtos
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Stock { get; set; } = string.Empty;
    }
}
