namespace OMX.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OMX.Domain;

    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder
                .Property(x => x.Title)
                .IsRequired(true)
                .HasMaxLength(DataModelConstants.SubCategoryTitleMaxLength);
        }
    }
}
