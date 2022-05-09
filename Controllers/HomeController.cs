
using BackEndFinalExam.DAL;
using BackEndFinalExam.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinalExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly BackEndExamDbContext _context;
        public HomeController(BackEndExamDbContext context)
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
