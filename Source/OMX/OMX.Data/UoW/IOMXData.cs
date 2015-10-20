namespace OMX.Data.UoW
{
    using OMX.Data.Repositories;
    using OMX.Models;

    public interface IOMXData
    {
        IOMXDbContext Context { get; }

        IDeletableEntityRepository<Ad> Ads { get; }

        IDeletableEntityRepository<Category> Categories { get; }

        IDeletableEntityRepository<SubCategory> SubCategories { get; }

        IDeletableEntityRepository<Picture> Pictures { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}