using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IOrangeBricksContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Property> Properties { get; set; }
        public IDbSet<Offer> Offers { get; set; }

        public async Task<int> SaveChangesToDbAsync()
        {
            int saved = await base.SaveChangesAsync();
            return saved;
        }
    }

    public interface IOrangeBricksContext
    {
        IDbSet<Property> Properties { get; set; }
        IDbSet<Offer> Offers { get; set; }

        Task<int> SaveChangesToDbAsync();
    }
}