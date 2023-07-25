using Microsoft.EntityFrameworkCore;
using UnitTestingLearn.DataLayer.Models;

namespace UnitTestingLearn.DataLayer
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<Category> Categories { set; get; }
    }
}
