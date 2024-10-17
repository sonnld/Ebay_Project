using Ebay_Project_PRN.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ebay_Project_PRN.Pages
{
    public class ManageCategoriesModel : PageModel
    {
        private readonly EBay_ProjectContext _context;
        [BindProperty]
        public Category Category { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public ManageCategoriesModel(EBay_ProjectContext context)
        {
            _context = context;
        }

        public List<Category> Categories { get; set; } = new List<Category>();

        // OnGetAsync - Hiển thị danh sách Category
        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                Categories = _context.Categories
                    .Where(c => c.CategoryName.Contains(SearchQuery))
                    .ToList();
            }
            else
            {
                // Nếu không có từ khóa thì lấy toàn bộ danh mục
                Categories = _context.Categories.ToList();
            }
        }

      

        // OnPostCreate - Thêm Category mới
        public async Task<IActionResult> OnPostCreate(Category category)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userName = User.Identity.Name;
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return Page();
            }
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.OwnerId == user.Id);

            if (store == null)
            {
                ModelState.AddModelError(string.Empty, "No store found for the logged-in user.");
                return Page();
            }

            category.IsActive = true; // Danh mục mặc định là hoạt động
            category.CreatedAt = DateTime.Now;
            category.StoreId = store.StoreId;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        // OnPostUpdate - Cập nhật thông tin Category
        public async Task<IActionResult> OnPostUpdate(Category category)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingCategory = await _context.Categories.FindAsync(category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.Description = category.Description;
                existingCategory.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        // OnPostToggleActive - Chuyển đổi trạng thái IsActive
        public async Task<IActionResult> OnPostToggleActive(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                category.IsActive = !category.IsActive; // Chuyển đổi trạng thái
                category.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}