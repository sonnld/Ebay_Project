﻿using Ebay_Project_PRN.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ebay_Project_PRN.Pages
{
    public class ManageCategoriesModel : PageModel
    {
        private readonly EBay_ProjectContext _context;
        private readonly UserManager<AspNetUser> _userManager;
        [BindProperty]
        public Category Category { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public ManageCategoriesModel(UserManager<AspNetUser> userManager, EBay_ProjectContext context)
        {
            _context = context;
            _userManager = userManager;
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
           
            // Retrieve the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Find the store associated with the logged-in user
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.OwnerId == user.Id);

            if (store == null)
            {
                // If the user does not have an associated store, return an error
                ModelState.AddModelError(string.Empty, "No store found for the logged-in user.");
                return Page();
            }
            category.StoreId = store.StoreId;
            category.IsActive = true; // Danh mục mặc định là hoạt động
            category.CreatedAt = DateTime.Now;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        // OnPostUpdate - Cập nhật thông tin Category
        public async Task<IActionResult> OnPostUpdate(Category category)
        {
            // Retrieve the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Find the store associated with the logged-in user
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.OwnerId == user.Id);

            if (store == null)
            {
                // If the user does not have an associated store, return an error
                ModelState.AddModelError(string.Empty, "No store found for the logged-in user.");
                return Page();
            }
            
            var existingCategory = await _context.Categories.FindAsync(category.CategoryId);
            if (existingCategory != null)
            {
                category.CategoryId = existingCategory.CategoryId;
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