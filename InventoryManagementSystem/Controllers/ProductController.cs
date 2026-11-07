using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagementSystem.Interfaces;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        public ProductController(IProductService productService, ICategoryService categoryService, ISupplierService supplierService)
        {

            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? categoryId, int? supplierId)
        {
            var products = await _productService.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = await _productService.SearchByName(searchString);
            }

            else if (categoryId.HasValue)
            {
                products = await _productService.FilterByCategoryId((int)categoryId);
            }
            else if (supplierId.HasValue)
            {
                products = await _productService.FilterBySupplierId((int)supplierId);
            }

            ViewBag.Categories = await _categoryService.GetAll();
            ViewBag.Suppliers = await _supplierService.GetAll();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductDetailsById((int)id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAll(), "Id", "Name");
            ViewBag.SupplierId = new SelectList(await _supplierService.GetAll(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.CategoryId == 0 || product.SupplierId == 0)
                {
                    ModelState.AddModelError(string.Empty, "Please select a valid category and supplier.");
                    ViewBag.CategoryId = new SelectList(await _categoryService.GetAll(), "Id", "Name");
                    ViewBag.SupplierId = new SelectList(await _supplierService.GetAll(), "Id", "Name");
                    return View(product);
                }
                _productService.Add(product);
                await _productService.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(await _categoryService.GetAll(), "Id", "Name");
            ViewBag.SupplierId = new SelectList(await _supplierService.GetAll(), "Id", "Name");

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetById((int)id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(await _categoryService.GetAll(), "Id", "Name");
            ViewBag.SupplierId = new SelectList(await _supplierService.GetAll(), "Id", "Name");

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productService.Update(product);
                await _productService.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(await _categoryService.GetAll(), "Id", "Name");
            ViewBag.SupplierId = new SelectList(await _supplierService.GetAll(), "Id", "Name");

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductDetailsById((int)id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productService.GetById(id);

            if (product != null)
            {
                _productService.Delete(product);
                await _productService.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}