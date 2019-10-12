namespace OMX.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OMX.Domain;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(x => x.Title)
                .IsRequired(true)
                .HasMaxLength(DataModelConstants.CategoryTitleMaxLength);
        }
    }
}
