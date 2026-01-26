using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streaming.Domain.Entities;

public class WatchHistoryConfiguration : IEntityTypeConfiguration<WatchHistory>
{
    public void Configure(EntityTypeBuilder<WatchHistory> builder)
    {
        builder.ToTable("watch_histories");
        
        builder.HasKey(wh => wh.Id);
        
        builder.HasOne(wh => wh.Profile).
            WithMany(p => p.WatchHistories).
            HasForeignKey(wh => wh.ProfileId);
        
        builder.HasOne(wh => wh.Episode)
            .WithMany()
            .HasForeignKey(wh => wh.EpisodeId)
            .IsRequired(false);
    }
}