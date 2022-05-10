using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinalExam.Models
{
    public class Product:BaseEntity
    {
        [StringLength(255)]
        public string Name { get; set; }
        [Column(TypeName="money")]
        public double Price { get; set; }
        [Column(TypeName = "money")]
        public double DiscountPrice { get; set; }
        public bool Aviability{ get; set; }
        public bool IsBestSeller{ get; set; }
        [Column(TypeName = "money")]
        public double EcoTax { get; set; }
        [Column(TypeName = "money")]
        public double VAT { get; set; }
        //[StringLength(1000), Required]
        //public string MainImage{ get; set; }
        public int CategoryId { get; set; }
        [StringLength(1000), Required]
        public string Description { get; set; }
        public string MainImage { get; set; }

        public Category Category { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        [NotMapped]

        public IFormFile MainImageFile { get; set; }
        [NotMapped]
        public IFormFile[] ProductImagesFile { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        [NotMapped]
        public List<int> Counts { get; set; } = new List<int>();


        [NotMapped]
        public List<int> SizeIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int> ColorIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int> TagIds { get; set; } = new List<int>();
      
        public int Count { get; set; }
        public IEnumerable<ProductSizeColor> ProductSizeColors{ get; set; }
        public IEnumerable<Basket> Baskets { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }

    }
}
