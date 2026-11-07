using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagementSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Controllers
{
    
    public class StockTransactionController : Controller
    {
        private readonly IStockTransactionService _stockTransactionService;
        private readonly IProductService _productService;
        public StockTransactionController(IStockTransactionService stockTransactionService, IProductService productService)
        {
            _stockTransactionService = stockTransactionService;
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _stockTransactionService.GetAll();

            return View(transactions);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.ProductId = new SelectList(await _productService.GetAll(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.GetById(transaction.ProductId);

                if (product == null)
                {
                    return NotFound();
                }

                if (transaction.TransactionType == "Stock In")
                {
                    product.Quantity += transaction.Quantity;
                }
                else if (transaction.TransactionType == "Stock Out")
                {
                    if (transaction.Quantity > product.Quantity)
                    {
                        ModelState.AddModelError("Quantity", "Quantity cannot exceed available stock.");

                        ViewBag.Products = _productService.GetAll();
                        return View(transaction);
                    }

                    product.Quantity -= transaction.Quantity;
                }

                transaction.TransactionDate = DateTime.Now;

                _stockTransactionService.Add(transaction);
                await _stockTransactionService.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ProductId = new SelectList(await _productService.GetAll(), "Id", "Name");

            return View(transaction);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _stockTransactionService.GetTransactionDetails((int)id);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _stockTransactionService.GetById(id);

            if (transaction != null)
            {
                _stockTransactionService.Delete(transaction);
                await _stockTransactionService.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}