using BackEndFinalExam.DAL;
using BackEndFinalExam.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinalExam.Controllers
{
    public class ShopController : Controller
    {
        private readonly BackEndExamDbContext _context;
        public ShopController(BackEndExamDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _context.Sliders.ToList(),

                Products = _context.Products.ToList()
            };
            return View(homeVM);
        }
    }
}
