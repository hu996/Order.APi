using Order.Core.Entities;

namespace Order.Repository.Repositories.ProductRepository
{
    public interface IProductRepo
    {
        Task<Product>createProductAsync(Product product);
        Task<Product>GetProductByIdAsync(int id);
       
        Task<IEnumerable<Product>>GetAllProductsAsync();
        Task<Product> UpdateProductAsync(Product mapprdct);
    }
}
