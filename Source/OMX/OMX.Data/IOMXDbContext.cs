namespace OMX.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using OMX.Models;

    public interface IOMXDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<SubCategory> SubCategories { get; set; }

        IDbSet<Ad> Ads { get; set; }

        IDbSet<Picture> Pictures { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}