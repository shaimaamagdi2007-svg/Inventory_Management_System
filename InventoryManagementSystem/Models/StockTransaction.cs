using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class StockTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int ProductId { get; set; }

        [Required]
        public string TransactionType { get; set; }

        
        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public Product? Product { get; set; }
    }
}
