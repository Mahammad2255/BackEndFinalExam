using BackEndFinalExam.DAL;
using BackEndFinalExam.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinalExam.Areas.Manage.Controllers
{
    [Area("manage")]

    public class CategoryController : Controller
    {
        private readonly BackEndExamDbContext _context;

        public CategoryController(BackEndExamDbContext context)
        {
            _context = context;
          
        }
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Category category)
        {



            if (!ModelState.IsValid) return View();

            category.CreatedAt = DateTime.UtcNow.AddHours(4);


            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();



            return RedirectToAction("Index");
        }
    }
}
