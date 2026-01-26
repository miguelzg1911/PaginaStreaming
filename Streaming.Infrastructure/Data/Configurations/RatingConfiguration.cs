using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streaming.Domain.Entities;

namespace Streaming.Infrastructure.Data.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("ratings");
        
        builder.HasKey(r => r.Id);
        
        builder.HasOne(r => r.Profile)
            .WithMany(p => p.Ratings)
            .HasForeignKey(r => r.ProfileId);
    }
}