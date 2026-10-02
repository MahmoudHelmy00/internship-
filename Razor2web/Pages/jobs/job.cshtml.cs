using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor2web.data;
using Razor2web.models;

namespace Razor2web.Pages.jobs
{
    public class jobModel : PageModel
    {
        private readonly appDbContext _dbContext;
        public jobModel(appDbContext dbContext)
        {
            _dbContext = dbContext;
        }      
        public job job { get; set; }
        public IActionResult OnGet(int id)
        {
            job = _dbContext.jobs.Find(id);

            if (job == null)
            {
            return NotFound();
            }
            return Page();
        }
    }
}
