namespace OMX.Data.Repositories
{
    using System.Linq;

    using OMX.Contracts;

    public interface IDeletableEntityRepository<T> : IRepository<T> where T : IEntity
    {
        IQueryable<T> AllWithDeleted();
    }
}
