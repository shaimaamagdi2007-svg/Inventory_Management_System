using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Interfaces
{
    public interface IProductService
    {
        public void Add(Product product);
        public void Update(Product product);
        public void Delete(Product product);
        public Task<Product> GetById(int id);
        public Task<Product> GetProductDetailsById(int id);
        public Task<List<Product>> SearchByName(string name);
        public Task<List<Product>> FilterByCategoryId(int categoryId);
        public Task<List<Product>> FilterBySupplierId(int supplierId);
        public Task<List<Product>> GetAll();
        public  Task SaveChanges();

    }
}
