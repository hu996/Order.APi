using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using Order.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Repositories.CustomerRepository
{
    public class Customerservice : ICustomerRepo
    {
        private readonly ApplicationDbContext _context;
        public Customerservice(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task<Customer> CreateAsync(Customer entity)
        {
            try
            {
                var newcustomer = await _context.Customers.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception("Error occurred while creating the customer", ex);
            }
           
        }

        public async Task<IEnumerable<Customer>> GeTAsync(int Id)
        {
            try
            {
               return await _context.Customers
                                    .Include(a=>a.Orders)
                                    .AsNoTracking()
                                    .Where (a=>a.CustomerId==Id)
                                    .ToListAsync();
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting the customer", ex);
            }
        }
    }
}
