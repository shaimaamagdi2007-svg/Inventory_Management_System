using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagementSystem.Services
{
    public class StockTransactionService : IStockTransactionService
    {
        private readonly ApplicationDbContext _context;
        public StockTransactionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(StockTransaction stockTransaction)
        {
            _context.StockTransactions.Add(stockTransaction);
        }
        public void Delete(StockTransaction stockTransaction)
        {
            _context.StockTransactions.Remove(stockTransaction);
        }
        public async Task<StockTransaction> GetById(int id)
        {
            return await _context.StockTransactions.FirstOrDefaultAsync(st => st.TransactionId == id);
        }
        public async Task<StockTransaction> GetTransactionDetails(int id)
        {
           var transactions = await _context.StockTransactions
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.TransactionId == id);
            return transactions;
        }
        public async Task<List<StockTransaction>> GetAll()
        {
            var transactions = await _context.StockTransactions
                .Include(s => s.Product)
                .OrderByDescending(s => s.TransactionDate)
                .ToListAsync();
            return transactions;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
