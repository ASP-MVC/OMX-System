namespace OMX.Contracts.Repositories
{
    using System.Linq;

    public interface IDeletableEntityRepository<T> : IRepository<T> where T : IEntity
    {
        IQueryable<T> AllWithDeleted();
    }
}
