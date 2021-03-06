﻿namespace OMX.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using OMX.Contracts.CodeFirstConventions;
    using OMX.Contracts.Models;
    using OMX.Data.Migrations;
    using OMX.Models;

    public class OMXDbContext : IdentityDbContext<User>, IOMXDbContext
    {
        public OMXDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OMXDbContext, Configuration>());
        }

        public virtual  IDbSet<Category> Categories { get; set; }

        public virtual  IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<SubCategory> SubCategories { get; set; }

        public virtual IDbSet<Ad> Ads { get; set; }

        public virtual IDbSet<Picture> Pictures { get; set; }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static OMXDbContext Create()
        {
            return new OMXDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

            modelBuilder.Entity<Comment>()
                .HasRequired(x => x.Author)
                .WithMany(x => x.SendComments)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Comment>()
                .HasOptional(x => x.Recipient)
                .WithMany(x => x.RecievedComments)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in this.ChangeTracker.Entries()
                    .Where(e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}