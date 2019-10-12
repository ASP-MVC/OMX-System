namespace OMX.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OMX.Domain;

    public class AdConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.Property(x => x.OwnerId).IsRequired(true);
            builder.Property(x => x.Title).HasMaxLength(DataModelConstants.AdTitleMaxLength);
            builder.Property(x => x.Title).IsRequired(true);
            builder.Property(x => x.Content).HasMaxLength(DataModelConstants.AdContentMaxLength);
        }
    }
}
