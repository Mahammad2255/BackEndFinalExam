using BackEndFinalExam.DAL;
using BackEndFinalExam.Models;
using BackEndFinalExam.ViewModel.Basket;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinalExam.Services
{
    public class LayoutService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BackEndExamDbContext _context;

        public LayoutService(IHttpContextAccessor httpContextAccessor, BackEndExamDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<List<BasketVM>> GetBasket()
        {
            string cookieBasket = _httpContextAccessor.HttpContext.Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(cookieBasket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookieBasket);
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }

            foreach (BasketVM basketVM in basketVMs)
            {
                Product dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.ProductId);
                basketVM.Image = dbProduct.MainImage;
                basketVM.Price = dbProduct.DiscountPrice > 0 ? dbProduct.DiscountPrice : dbProduct.Price;
                basketVM.EcoTax = dbProduct.EcoTax;
                basketVM.VAT = dbProduct.VAT;
                basketVM.Name = dbProduct.Name;
            }

            return basketVMs;
        }

        public async Task<Setting> GetSetting()
        {
            return await _context.Settings.FirstOrDefaultAsync();
        }
    }
  
}
