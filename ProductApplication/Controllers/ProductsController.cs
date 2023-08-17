using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Context;
using ProductApplication.Models;

namespace ProductApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly EcommerceDbContext _context;

        public ProductsController(EcommerceDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? categoryId, string sortOrder)
        {
            var products = _context.Products.Include(p => p.Category).ToList();

            if (categoryId.HasValue)
            {
                products = _context.Products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }
            switch (sortOrder)
            {
                case "price_asc":
                    products = _context.Products.OrderBy(p => p.Price).ToList();
                    break;
                case "price_desc":
                    products = _context.Products.OrderByDescending(p => p.Price).ToList();
                    break;
                case "name_asc":
                    products =_context.Products.OrderBy(p => p.Name).ToList();
                    break;
                case "name_desc":
                    products = _context.Products.OrderByDescending(p => p.Name).ToList();
                    break;
                default:
                    // Default sort order, if not specified
                    products = _context.Products.OrderBy(p => p.Id).ToList();
                    break;
            }

            ViewBag.Categories = new SelectList(_context.Category, "Id", "Name");
          
            return View(products);
        }

        public IActionResult FilteredProducts(int category, string sortOrder)
        {
            var products = _context.Products.AsQueryable(); // Start with all products

            if (category != 0) // Assuming 0 represents "All Categories"
            {
                products = products.Where(p => p.CategoryId == category);
            }

            // Apply sorting based on sortOrder parameter
            if (sortOrder == "asc")
            {
                products = products.OrderBy(p => p.Name);
            }
            else if (sortOrder == "desc")
            {
                products = products.OrderByDescending(p => p.Name);
            }else
            {
                products = products.OrderBy(p => p.Id);
            }

            // Convert the filtered and sorted products to a list and then to JSON
            var filteredAndSortedProducts = products.ToList();
            return Json(filteredAndSortedProducts);
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            try
            {
                ViewBag.Categories = _context.Category.ToList();
                return View();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                return RedirectToAction("Error", "Home"); // Redirect to an error page
            }
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId")] Product product)
        {
            
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            //ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_context.Category, "Id", "Name", product.CategoryId);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

          
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            //ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            //return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'EcommerceDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
