using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts() => 
            View(await _context.Products
                .Include(p => p.ProductType)
                .ToListAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> BuyProduct(int id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync();
            return View(product);
        }


        [HttpGet]
        public async Task<IActionResult> Search(string search)
        {
            if(search is null)
                return RedirectToAction("GetProducts");
                
            var products = await _context.Products
                .Where(p => p.Name.ToLower().Contains(search.ToLower())||
                p.ProductType.Name.ToLower().Contains(search.ToLower()))
                .Include(p => p.ProductType)
                .ToListAsync();
            return View(products);
        }


    }
}