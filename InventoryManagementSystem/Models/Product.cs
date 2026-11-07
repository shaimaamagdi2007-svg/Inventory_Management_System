using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int RecorderLevel { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
   
        public int CategoryId { get; set; }
       
        public int SupplierId { get; set; }
        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }
        public ICollection<StockTransaction>? StockTransactions { get; set; }

    }
}
