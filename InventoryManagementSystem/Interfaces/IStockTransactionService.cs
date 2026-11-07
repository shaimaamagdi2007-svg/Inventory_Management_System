using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Interfaces
{
    public interface IStockTransactionService
    {
        public void Add(StockTransaction stockTransaction);
        public void Delete(StockTransaction stockTransaction);
        public Task<StockTransaction> GetById(int id);
        public Task<StockTransaction> GetTransactionDetails(int id);
        public Task<List<StockTransaction>> GetAll();


        public Task SaveChanges();
    }
}
