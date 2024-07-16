using System.ComponentModel.DataAnnotations;

namespace Order.Core.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        private decimal _discount;

        public decimal Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        public OrderItem()
        {
            // Initialize discount to 0 by default
            _discount = 0;
        }

        // Method to calculate discount based on some criteria
        public void CalculateDiscount()
        {
            decimal totalPrice = Quantity * UnitPrice;

            if (totalPrice > 200)
            {
                _discount = totalPrice * 0.1m; // 10% discount
            }
            else if (totalPrice < 100)
            {
                _discount = totalPrice * 0.05m; // 5% discount
            }
            else
            {
                _discount = 0; // No discount
            }
        }
    }
}
