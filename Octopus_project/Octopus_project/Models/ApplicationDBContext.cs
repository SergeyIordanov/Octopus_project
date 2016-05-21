using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Octopus_project.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Photo> Photos { set; get; }

        public DbSet<Like> Likes { set; get; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}