using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streaming.Domain.Entities;

namespace Streaming.Infrastructure.Data.Configurations;

public class ContentGenreConfiguration : IEntityTypeConfiguration<ContentGenre>
{
    public void Configure(EntityTypeBuilder<ContentGenre> builder)
    {
        builder.ToTable("content_genres");
        
        builder.HasKey(cg => new { cg.ContentId, cg.GenreId });
    }
}