using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using Order.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Repositories.ProductRepository
{
    public class ProductServices : IProductRepo
    {

        private readonly ApplicationDbContext _context;
        public ProductServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> createProductAsync(Product product)
        {
            try
            {
               await _context.products.AddAsync(product);
               await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Ocuured While Create Product", ex);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IQueryable products= (IQueryable)await _context.products.AsNoTracking().ToListAsync();
            if(products is not null)
            {
                return (IEnumerable<Product>)products;
            }
            else
            {
                return Enumerable.Empty<Product>(); 
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return  _context.products.Find(id);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            try
            {
                var prdct = await _context.products.FindAsync(product.ProductId);
                if (prdct is not null)
                {
                    prdct.Price = product.Price;
                    prdct.Stock = product.Stock;
                    prdct.Name = product.Name;
                    
                     _context.products.Update(prdct);
                     await _context.SaveChangesAsync();
                }
                return prdct;
            }

            catch(Exception ex)
            {
                throw new ArgumentException($"Product with ID {product.ProductId} not found.");
            }
           
        }
    }
}
