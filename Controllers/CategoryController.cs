using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;

namespace Shop.Controllers
{
    public class CategoryController : Controller
    {
        private DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("prType/{productTypeId}")]
        public async  Task<IActionResult> GetProductByProductTypeId(int productTypeId)
        {
            var products = await _context.Products
                .Where(p => p.ProductTypeId == productTypeId)
                .Include(p => p.ProductType)
                .ToListAsync();
            return View(products);
        }

        [HttpGet("cat/{categoryName}")]
        public async Task<IActionResult> GetProductsByCategoryName(string categoryName)
        {
            var products = await _context.Products
                .Where(p => p.ProductType.Name.ToLower().Equals(categoryName))
                .Include(p => p.ProductType)
                .ToListAsync();
            return View(products);
        }
    }
}