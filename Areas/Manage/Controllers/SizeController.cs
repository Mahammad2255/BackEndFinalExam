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

    public class SizeController : Controller
    {
        private readonly BackEndExamDbContext _context;

        public SizeController(BackEndExamDbContext context)
        {
            _context = context;
          
        }
        public IActionResult Index()
        {
            return View(_context.Sizes.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Size size)
        {



            if (!ModelState.IsValid) return View();

            //size.CreatedAt = DateTime.UtcNow.AddHours(4);


            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();



            return RedirectToAction("Index");
        }
    }
}
