using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Interfaces
{
    public interface ICategoryService
    {
        public void Add(Category category);
        public void Update(Category category);
        public void Delete(Category category);
        public Task<Category> GetById(int id);

        public Task<List<Category>> GetAll();


        public Task SaveChanges();
    }
}
