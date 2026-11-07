using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagementSystem.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;
        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
        }
        public void Update(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
        }
        public void Delete(Supplier supplier)
        {
            _context.Suppliers.Remove(supplier);
        }
        public async Task<Supplier> GetById(int id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<List<Supplier>> GetAll()
        {
            return await _context.Suppliers.ToListAsync();
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
