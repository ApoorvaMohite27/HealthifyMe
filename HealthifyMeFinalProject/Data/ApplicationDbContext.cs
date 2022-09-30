using HealthifyMeFinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthifyMeFinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>         /*DbContext*/
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerDetail> CustomerDetails { get; set; }

        public DbSet<Dietitian> Dietitians { get; set; }

        public DbSet<AssignTask> AssignTasks { get; set; }

        public DbSet<FeedBackForm> FeedBackForms { get; set; }
    }
}
