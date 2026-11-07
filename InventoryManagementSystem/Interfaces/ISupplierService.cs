using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Interfaces
{
    public interface ISupplierService
    {
        public void Add(Supplier supplier);
        public void Update(Supplier supplier);
        public void Delete(Supplier supplier);
        public Task<Supplier> GetById(int id);
        
        public Task<List<Supplier>> GetAll();
        

        public Task SaveChanges();
    }
}
