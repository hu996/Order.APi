using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using Order.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Repositories.OrderRepository
{
    public class OrderServices : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderServices(ApplicationDbContext context)
        {
            _context=context;   
        }
        public async Task<Orders> CreateOrderAsync(Orders order)
        {
            try
            {
                order.OrderDate= DateTime.Now;
                order.TotalAmount= 0;
                var createdOrder = await _context.orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return createdOrder.Entity;
                

            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }

       

        public async Task<IEnumerable<Orders>> GetAllOrdersAsync()
        {
            return await _context.orders
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<Orders> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Orders> UpdateOrderAsync(int orderid ,string status)
        {
            try
            {
                var order = await _context.orders.FindAsync(orderid);
                if (order is not null)
                {
                    order.Status = status;
                    await _context.SaveChangesAsync();
                    return order;
                }
                return null;
            }
           
            catch(Exception ex)
            {
                throw ex;
            }
        }

        Task<Orders> IOrderRepository.GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
