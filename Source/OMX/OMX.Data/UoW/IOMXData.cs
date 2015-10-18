namespace OMX.Data.UoW
{
    using OMX.Data.Repositories;
    using OMX.Models;

    public interface IOMXData
    {
        IOMXDbContext Context { get; }

        //IDeletableEntityRepository<Advertisement> Advertisements { get; }

        //IDeletableEntityRepository<Category> Categories { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}