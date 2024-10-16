using Ebay_Project_PRN.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ebay_Project_PRN.Pages
{
    public class ManageProductModel : PageModel
    {
        private readonly EBay_ProjectContext _context;

        public ManageProductModel(EBay_ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync(string? searchQuery)
        {
            Products = string.IsNullOrEmpty(searchQuery)
                ? await _context.Products.Include(p => p.ProductImages).ToListAsync()
                : await _context.Products
                    .Include(p => p.ProductImages)
                    .Where(p => p.ProductName.Contains(searchQuery))
                    .ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync(List<IFormFile> ProductImages)
        {
            if (!ModelState.IsValid) return Page();

            // Save product
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            // Save product images
            foreach (var image in ProductImages)
            {
                if (image.Length > 0)
                {
                    var imageUrl = await SaveImageAsync(image); // Implement this method to save images
                    var productImage = new ProductImage { ProductId = Product.ProductId, ImageUrl = imageUrl };
                    _context.ProductImages.Add(productImage);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int productId, string productName, decimal price, int stockQuantity, string description)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            product.ProductName = productName;
            product.Price = price;
            product.StockQuantity = stockQuantity;
            product.Description = description;

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleActiveAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            product.IsActive = !product.IsActive; // Toggle active status
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            // Implement your image saving logic here, returning the URL or path to the image.
            return "ImagePath"; // Replace with actual image path
        }
    }
}