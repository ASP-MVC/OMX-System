namespace OMX.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OMX.Domain;

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Content).HasMaxLength(DataModelConstants.CommentContentMaxLength);
            builder.Property(x => x.AuthorId).IsRequired(true);
        }
    }
}
