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

    public class ColorController : Controller
    {
        private readonly BackEndExamDbContext _context;

        public ColorController(BackEndExamDbContext context)
        {
            _context = context;
          
        }
        public IActionResult Index()
        {
            return View(_context.Colors.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Color color)
        {



            if (!ModelState.IsValid) return View();

            //color.CreatedAt = DateTime.UtcNow.AddHours(4);


            await _context.Colors.AddAsync(color);
            await _context.SaveChangesAsync();



            return RedirectToAction("Index");
        }
    }
}
