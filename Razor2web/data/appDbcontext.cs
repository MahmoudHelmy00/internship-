using Microsoft.EntityFrameworkCore;
using Razor2web.models;
using System;

namespace Razor2web.data
{
    public class appDbContext:DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> option) : base(option)
        {

        }
        public DbSet<Employee> Employeest{ get; set; }
        public DbSet<job> jobs{ get; set; }

    }
   
}
