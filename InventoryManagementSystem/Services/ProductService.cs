using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }
        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Product> GetProductDetailsById(int id)
        {
            return await _context.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> SearchByName(string name)
        {
            return await _context.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }
        public async Task<List<Product>> FilterByCategoryId(int categoryId)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }
        public async Task<List<Product>> FilterBySupplierId(int supplierId)
        {
            return await _context.Products.Where(p => p.SupplierId == supplierId).ToListAsync();
        }
        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
