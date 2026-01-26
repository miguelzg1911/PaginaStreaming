using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streaming.Domain.Entities;

namespace Streaming.Infrastructure.Data.Configurations;

public class MyListConfiguration : IEntityTypeConfiguration<MyList>
{
    public void Configure(EntityTypeBuilder<MyList> builder)
    {
        builder.ToTable("my_lists");
        
        builder.HasKey(ml => new { ml.ProfileId, ml.ContentId });
    }
}