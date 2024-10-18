using Ebay_Project_PRN.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Ebay_Project_PRN.Pages
{
    public class ManageProductModel : PageModel
    {
        private readonly EBay_ProjectContext _context;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        public ManageProductModel(UserManager<AspNetUser> userManager, EBay_ProjectContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; }
        public SelectList Categories { get; set; }
        [BindProperty]
        public IFormFileCollection UploadedImages { get; set; }
       
        public List<Product> Products { get; set; } = new List<Product>();
        public async Task OnGetAsync(string? searchQuery, int? categoryId)
        {


            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Handle the case where the user is not logged in
                RedirectToPage("/Account/Login", new { area = "Identity" });
                return;
            }

            // Find the store associated with the logged-in user
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.OwnerId == user.Id);
            if (store == null)
            {
                // Handle the case where the user does not have an associated store
                ModelState.AddModelError(string.Empty, "Store not found for the logged-in user.");
                return;
            }

            // Filter categories by StoreId
            Categories = new SelectList(await _context.Categories
                .Where(c => c.StoreId == store.StoreId)
                .ToListAsync(), "CategoryId", "CategoryName");

            // Filter products by search query if provided
            Products = await GetProductsByStoreAsync(store.StoreId, searchQuery, categoryId);
        }
        public async Task<List<Product>> GetProductsByStoreAsync(int storeId, string? searchQuery, int? categoryId)
        {
            // Lọc sản phẩm theo search query và category
            var productsQuery = _context.Products
                .Include(p => p.ProductImages)
                .Where(p => p.StoreId == storeId);

            if (!string.IsNullOrEmpty(searchQuery))
            {
                productsQuery = productsQuery.Where(p => p.ProductName.Contains(searchQuery));
            }

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            return await productsQuery.ToListAsync();
        }


        public async Task<IActionResult> OnPostCreateAsync(string categoryName)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Find the user's store
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.OwnerId == user.Id);
            if (store == null)
            {
                ModelState.AddModelError("", "Store not found for the logged-in user.");
                return Page();
            }

            // Find the selected category
            var category = await _context.Categories.FindAsync(Product.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError("", "Category not found!");
                return Page();
            }

            // Assign the product details
            Product.CategoryId = category.CategoryId; // This line is redundant, you can remove it.
            Product.StoreId = store.StoreId;

            // Save the product
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            // Handle uploaded images (if any)
            if (UploadedImages != null && UploadedImages.Any())
            {
                foreach (var image in UploadedImages)
                {
                    var imagePath = await SaveImageAsync(image);
                    var productImage = new ProductImage
                    {
                        ProductId = Product.ProductId,
                        ImageUrl = imagePath
                    };
                    _context.ProductImages.Add(productImage);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var fileName = Path.GetFileNameWithoutExtension(image.FileName);
            var extension = Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(_environment.WebRootPath, "uploads", fileName + extension);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return $"/uploads/{fileName}{extension}";
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

      
    }
}