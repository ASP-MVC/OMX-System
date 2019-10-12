namespace OMX.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using OMX.MVC.Persistence;
    using OMX.Persistence.Infrastructure;

    public class OMXDbContextFactory : DesignTimeDbContextFactoryBase<OMXDbContext>
    {
        protected override OMXDbContext CreateNewInstance(DbContextOptions<OMXDbContext> options)
        {
            return new OMXDbContext(options);
        }
    }
}
