using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streaming.Domain.Entities;

namespace Streaming.Infrastructure.Data.Configurations;

public class WatchHistoryConfiguration : IEntityTypeConfiguration<WatchHistory>
{
    public void Configure(EntityTypeBuilder<WatchHistory> builder)
    {
        builder.ToTable("watch_histories");
        
        builder.HasKey(wh => wh.Id);
        
        builder.Property(wh => wh.WatchedSeconds)
            .HasColumnName("watched_seconds");
        
        builder.Property(wh => wh.LastWatchedAt)
            .HasColumnName("last_watched_at");
        
        builder.HasOne(wh => wh.Profile).
            WithMany(p => p.WatchHistories).
            HasForeignKey(wh => wh.ProfileId);
        
        builder.HasOne(wh => wh.Episode)
            .WithMany()
            .HasForeignKey(wh => wh.EpisodeId)
            .IsRequired(false);
    }
}