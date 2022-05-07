using BackEndFinalExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinalExam.ViewModel
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public Setting Setting{ get; set; }
        public List<Product> Products { get; set; }
    }
}
