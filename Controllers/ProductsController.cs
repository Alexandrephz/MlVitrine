using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MlVitrine.Data;
using MlVitrine.Models;
using MlVitrine.Services;

namespace MlVitrine.Controllers
{
[Authorize]
    public class ProductsController : Controller

    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MlVitrineContext _context;

        public ProductsController(MlVitrineContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment= webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var mlVitrineContext = _context.Product.Include(p => p.ProductCondition)
                .Include(p => p.ProductSpec)
                .Include(pI => pI.ProductImages)
                .Include(pB => pB.Brand)
                .Where(p => p.product_active == true).OrderByDescending(p => p.UpdatedDate);
            return View(await mlVitrineContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductCondition)
                .Include(p => p.ProductSpec)
                .Include(pI => pI.ProductImages)
                .Include(pB => pB.Brand)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductConditionId"] = new SelectList(_context.Set<ProductCondition>(), "product_condition", "product_condition");
            ViewData["ProductSpecId"] = new SelectList(_context.Set<ProductSpec>(), "product_attribute", "product_attribute");
            ViewData["BrandId"] = new SelectList(_context.Set<Brand>(), "brand_name", "brand_name");


            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,product_name,product_mainimage,product_model,product_description,product_sku,product_ean,BrandId,ProductSpecId,product_price, product_stock, product_active,ProductConditionId,CreatedDate,UpdatedDate")] Product product, IFormFile product_mainimage)
        {
            string image_upload = await ImageUpload.UploadToAzure(product_mainimage, product.product_sku + "_main.jpg");
            product.product_mainimage = image_upload;

            _context.Update(product);
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.UtcNow;
                product.UpdatedDate = DateTime.UtcNow;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductConditionId"] = new SelectList(_context.Set<ProductCondition>(), "ProductConditionId", "ProductConditionId", product.ProductConditionId);
            ViewData["ProductSpecId"] = new SelectList(_context.Set<ProductSpec>(), "ProductSpecId", "ProductSpecId", product.ProductSpecId);
            ViewData["BrandId"] = new SelectList(_context.Set<Brand>(), "BrandId", "BrandId", product.BrandId);

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductConditionId"] = new SelectList(_context.Set<ProductCondition>(), "ProductConditionId", "product_condition", product.ProductConditionId);
            ViewData["ProductSpecId"] = new SelectList(_context.Set<ProductSpec>(), "ProductSpecId", "product_attribute", product.ProductSpecId);
            ViewData["BrandId"] = new SelectList(_context.Set<Brand>(), "BrandId", "brand_name", product.BrandId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,product_name,product_model,product_description,product_sku," +
            "product_ean,BrandId,ProductSpecId,product_price,product_stock,product_active,ProductConditionId,CreatedDate,UpdatedDate")]Product product, IFormFile product_mainimage)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            if (product_mainimage != null) {
                string image_upload = await ImageUpload.UploadToAzure(product_mainimage, product.product_sku + "_main.jpg");
                product.product_mainimage = image_upload;
                _context.Update(product);
            }else
            {
                ModelState.Remove("product_mainimage");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(product).State = EntityState.Modified;
                    product.UpdatedDate = DateTime.UtcNow;
                    _context.Update(product);
                    _context.Entry(product).Property(x => x.CreatedDate).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductConditionId"] = new SelectList(_context.Set<ProductCondition>(), "ProductConditionId", "ProductConditionId", product.ProductConditionId);
            ViewData["ProductSpecId"] = new SelectList(_context.Set<ProductSpec>(), "ProductSpecId", "ProductSpecId", product.ProductSpecId);
            ViewData["BrandId"] = new SelectList(_context.Set<Brand>(), "BrandId", "BrandId", product.BrandId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductCondition)
                .Include(p => p.ProductSpec)
                .FirstOrDefaultAsync(m => m.ProductId == id);
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
            if (_context.Product == null)
            {
                return Problem("Entity set 'MlVitrineContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
