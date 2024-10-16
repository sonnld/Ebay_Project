using Ebay_Project_PRN.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ebay_Project_PRN.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly EBay_ProjectContext _context;

        public IndexModel(EBay_ProjectContext context)
        {
            _context = context;
        }

        public List<Store> Stores { get; set; }

        public async Task OnGetAsync()
        {
            Stores = await _context.Stores.ToListAsync();
        }
    }
}
