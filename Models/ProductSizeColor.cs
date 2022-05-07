using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinalExam.Models
{
    public class ProductSizeColor:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Nullable<int> SizeId { get; set; }
        public Size Size { get; set; }
        public Nullable<int> ColorId { get; set; }
        public Color Color { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
