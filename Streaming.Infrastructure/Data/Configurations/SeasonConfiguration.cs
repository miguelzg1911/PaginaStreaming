using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streaming.Domain.Entities;

namespace Streaming.Infrastructure.Data.Configurations;

public class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable("seasons");
        
        builder.HasKey(s => s.Id);
        
        builder.HasOne(s => s.Content)
            .WithMany(c => c.Seasons)
            .HasForeignKey(s => s.ContentId);
    }
}