namespace OMX.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using OMX.Models;

    public class OMXDbContext : IdentityDbContext<User>
    {
        public OMXDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static OMXDbContext Create()
        {
            return new OMXDbContext();
        }
    }
}