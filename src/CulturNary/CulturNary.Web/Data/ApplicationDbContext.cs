using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CulturNary.Web.Areas.Identity.Data;

namespace CulturNary.Web.Data;

public class ApplicationDbContext : IdentityDbContext<SiteUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
}
