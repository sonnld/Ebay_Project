using Ebay_Project_PRN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ebay_Project_PRN.Pages
{
    [Authorize]
    public class UserModel : PageModel
    {
        private readonly UserManager<User> userManager;
        public User? appUser;

        public UserModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public void OnGet()
        {
            var task = userManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
        }
    }
}
