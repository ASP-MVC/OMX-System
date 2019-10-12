namespace OMX.MVC.Persistence
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using OMX.Domain;

    public class OMXDbContext : IdentityDbContext
    {
        public OMXDbContext(DbContextOptions<OMXDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Ad> Ads { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Ad
            builder.Entity<Ad>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.PublishedAds)
                .HasForeignKey(x => x.OwnerId)
                .IsRequired(false);

            builder.Entity<Ad>()
               .HasOne(x => x.SubCategory)
               .WithMany(x => x.Ads)
               .HasForeignKey(x => x.SubCategoryId)
               .IsRequired(true);

            builder.Entity<Ad>()
               .HasMany(x => x.Comments)
               .WithOne(x => x.Ad)
               .HasForeignKey(x => x.AdId)
               .IsRequired(false);

            builder.Entity<Ad>()
               .HasMany(x => x.Pictures)
               .WithOne(x => x.Ad)
               .HasForeignKey(x => x.AdId)
               .IsRequired(false);

            // Category
            builder.Entity<Category>()
                .HasMany(x => x.SubCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired(false);

            // User
            builder.Entity<User>()
                .HasMany(x => x.UploadedPictures)
                .WithOne(x => x.Owner)
                .HasForeignKey(x => x.OwnerId)
                .IsRequired(false);

            builder.Entity<User>()
               .HasMany(x => x.RecievedComments)
               .WithOne(x => x.Recipient)
               .HasForeignKey(x => x.RecipientId)
               .IsRequired(false);

            builder.ApplyConfigurationsFromAssembly(typeof(OMXDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
 