using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streaming.Domain.Entities;

namespace Streaming.Infrastructure.Data.Configurations;

public class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.ToTable("contents");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ContentType)
            .HasConversion<string>();
        
        builder.Property(c => c.AgeRating).
            HasConversion<string>();
    }
}