using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor2web.data;
using Razor2web.models;

namespace Razor2web.Pages.jobs
{
    [IgnoreAntiforgeryToken]

    public class listModel : PageModel
    {
        private readonly appDbContext _dbContext;
        public listModel(appDbContext dbContext) 
        {
            _dbContext = dbContext;

        }

        public List<job> jobList { get; set; }
        
        public void OnGet()
        {
            jobList = _dbContext.jobs.ToList();
        }
        public JsonResult OnGetJobsJson()
        {
            var job = _dbContext.jobs.ToList();
            return new JsonResult(job);
        }

        public JsonResult OnPostAddJob([FromBody] jobToAdd job)
        {
            job newjob = new job();
             
            newjob.Name=job.Name;
            newjob.Description=job.Description;
            newjob.department=job.department;
            newjob.salary=job.salary;

            _dbContext.jobs.Add(newjob);
            _dbContext.SaveChanges();

            return new JsonResult(new { success = true });
        }
    }

    public class jobToAdd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string department { get; set; }
        public int salary { get; set; }
    }
}
