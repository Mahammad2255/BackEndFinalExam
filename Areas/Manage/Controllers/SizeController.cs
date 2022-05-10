using BackEndFinalExam.DAL;
using BackEndFinalExam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinalExam.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]

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

        public async Task<IActionResult> Update(int? id)
        {
            Size dbSize = await _context.Sizes.FirstOrDefaultAsync(c => c.Id == id);
            return View(dbSize);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, Size size)
        {
            Size dbSize = await _context.Sizes.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View();
            }


            dbSize.Name = dbSize.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Size size = await _context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (size == null) return NotFound();
            return View(size);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Size size = await _context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (size == null) return NotFound();
            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
