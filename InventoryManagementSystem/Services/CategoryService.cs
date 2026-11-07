using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagementSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }
        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }
        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
