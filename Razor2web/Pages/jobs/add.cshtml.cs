using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor2web.data;
using Razor2web.models;

namespace Razor2web.Pages.jobs
{
    public class addModel : PageModel
    {
        public readonly appDbContext appDbContext;
        public addModel(appDbContext dbContext)
        {
         appDbContext = dbContext;
        }
        public void OnGet()
        {
        }
        public job job { get; set; }
        [HttpPost]
        public void OnPost(job job) 
        {
            var jobVeiw = new job
            {
                Name = job.Name,
                salary = job.salary,
                department = job.department,
                Description=job.Description,
                Id = job.Id,
            };
            appDbContext.jobs.Add(jobVeiw);
            appDbContext.SaveChanges();
            ViewData["message"] = "employee has been created sussefully ";

        }
    }
}
