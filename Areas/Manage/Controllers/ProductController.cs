using BackEndFinalExam.DAL;
using BackEndFinalExam.Extentions;
using BackEndFinalExam.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinalExam.Areas.Manage.Controllers
{
    [Area("manage")]

    public class ProductController : Controller
    {
        private readonly BackEndExamDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(BackEndExamDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(bool? status, int page = 1)
        {
            ViewBag.Status = status;

            IEnumerable<Product> products = await _context.Products
                .Include(t => t.Category)
                .Include(p=>p.ProductSizeColors).ThenInclude(p=>p.Size)
                .Include(p=>p.ProductSizeColors).ThenInclude(p=>p.Color)
                

                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            ViewBag.PageIndex = page;
            ViewBag.PageCount = Math.Ceiling((double)products.Count() / 5);

            return View(products.Skip((page - 1) * 5).Take(5));
        }
        public async Task<IActionResult> Detail(int? id, bool? status, int page = 1)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products
               
                .Include(p => p.ProductImages)
                .Include(p => p.ProductSizeColors).ThenInclude(p => p.Size)
                .Include(p => p.ProductSizeColors).ThenInclude(p => p.Color)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Categories = await _context.Categories.Where(b => !b.IsDeleted).ToListAsync();
          
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
  
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product product, bool? status, int page = 1)
        {
          
            ViewBag.Categories = await _context.Categories.Where(b => !b.IsDeleted).ToListAsync();
            
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (product.ProductImagesFile.Count() > 6)
            {
                ModelState.AddModelError("ProductImagesFile", $"maksimum yukleyebileceyin say 6");
                return View();
            }

            if (product.SizeIds.Count != product.ColorIds.Count || product.SizeIds.Count != product.Counts.Count || product.Counts.Count != product.ColorIds.Count)
            {
                ModelState.AddModelError("", "Incorect");
                return View();
            }

            foreach (int item in product.SizeIds)
            {
                if (!await _context.Sizes.AnyAsync(s => s.Id == item))
                {
                    ModelState.AddModelError("", "Incorect Size Id");
                    return View();
                }
            }

            foreach (int item in product.ColorIds)
            {
                if (!await _context.Colors.AnyAsync(s => s.Id == item))
                {
                    ModelState.AddModelError("", "Incorect Color Id");
                    return View();
                }
            }

            //for (int i = 0; i < product.ColorIds.Count; i++)
            //{
            //    if (product.ColorIds.Where(c=>c == product.ColorIds[i]).Count() > 1 && product.SizeIds.Where(c => c == product.SizeIds[i]).Count() > 1)
            //    {

            //    }
            //}

            List<ProductSizeColor> productColorSizes = new List<ProductSizeColor>();

            for (int i = 0; i < product.ColorIds.Count; i++)
            {
                ProductSizeColor productColorSize = new ProductSizeColor
                {
                    ColorId = product.ColorIds[i],
                    SizeId = product.SizeIds[i],
                    Count = product.Counts[i]
                };

                productColorSizes.Add(productColorSize);
            }

            product.ProductSizeColors = productColorSizes;

            

            if (!await _context.Categories.AnyAsync(b => b.Id == product.CategoryId && !b.IsDeleted))
            {
                ModelState.AddModelError("CategoryId", "Duzgun Category Secin ");
                return View();
            }



            if (product.MainImageFile != null)
            {
                if (!product.MainImageFile.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("MainImageFile", "Secilen Seklin Novu Uygun");
                    return View();
                }

                if (!product.MainImageFile.CheckFileSize(300))
                {
                    ModelState.AddModelError("MainImageFile", "Secilen Seklin Olcusu Maksimum 30 Kb Ola Biler");
                    return View();
                }

                product.MainImage = product.MainImageFile.CreateFile(_env, "assets", "images", "product");
            }
            else
            {
                ModelState.AddModelError("MainImageFile", "Main Sekil Mutleq Secilmelidir");
                return View();
            }

           
            if (product.ProductImagesFile.Count() > 0)
            {
                List<ProductImage> productImages = new List<ProductImage>();

                foreach (IFormFile file in product.ProductImagesFile)
                {
                    if (!file.CheckFileContentType("image/jpeg"))
                    {
                        ModelState.AddModelError("ProductImagesFile", "Secilen Seklin Novu Uygun");
                        return View();
                    }

                    if (!file.CheckFileSize(300))
                    {
                        ModelState.AddModelError("ProductImagesFile", "Secilen Seklin Olcusu Maksimum 30 Kb Ola Biler");
                        return View();
                    }

                    ProductImage productImage = new ProductImage
                    {
                        Image = file.CreateFile(_env, "assets", "images", "product"),
                        CreatedAt = DateTime.UtcNow.AddHours(4)
                    };

                    productImages.Add(productImage);
                }

                product.ProductImages = productImages;
            }
            else
            {
                ModelState.AddModelError("ProductImagesFile", "Product Images File Sekil Mutleq Secilmelidir");
                return View();
            }

            product.Aviability = product.Count > 0 ? true : false;
            product.CreatedAt = DateTime.UtcNow.AddHours(4);
            product.Count = product.Counts.Sum();

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("index", new { status, page });
        }
        public async Task<IActionResult> GetFormColoRSizeCount()
        {
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();

            return PartialView("_ProductColorSizePartial");
        }
    }
}
