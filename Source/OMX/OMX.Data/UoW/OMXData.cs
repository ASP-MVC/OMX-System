namespace OMX.Data.UoW
{
    using System;
    using System.Collections.Generic;

    using OMX.Contracts;
    using OMX.Contracts.Models;
    using OMX.Data.Repositories;
    using OMX.Models;

    public class OMXData : IOMXData
    {
        private readonly IOMXDbContext context;

        private readonly Dictionary<Type, object> repositories;

        public OMXData(IOMXDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IOMXDbContext Context
        {
            get
            {
                return this.context;
            }
        }
        
        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IDeletableEntityRepository<Comment> Comments
        {
            get { return this.GetDeletableEntityRepository<Comment>(); }
        }

        public IDeletableEntityRepository<Ad> Ads
        {
            get { return this.GetDeletableEntityRepository<Ad>(); }
        }

        public IDeletableEntityRepository<Category> Categories
        {
            get { return this.GetDeletableEntityRepository<Category>(); }
        }

        public IDeletableEntityRepository<SubCategory> SubCategories
        {
            get { return this.GetDeletableEntityRepository<SubCategory>(); }
        }

        public IDeletableEntityRepository<Picture> Pictures
        {
            get { return this.GetDeletableEntityRepository<Picture>(); }
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class, IEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity, IEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}