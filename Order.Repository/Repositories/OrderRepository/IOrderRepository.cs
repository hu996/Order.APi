using Order.Core.Entities;

namespace Order.Repository.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task<Orders> CreateOrderAsync(Orders order);

        Task<Orders> UpdateOrderAsync(int orderid,string status);
        
        Task<Orders> GetOrderByIdAsync(int  id);
        Task<Orders> GetAllOrdersAsync();
    }
}
