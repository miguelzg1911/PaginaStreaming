using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streaming.Domain.Entities;

namespace Streaming.Infrastructure.Data.Configurations;

public class SuscriptionConfiguration : IEntityTypeConfiguration<Suscription>
{
    public void Configure(EntityTypeBuilder<Suscription> builder)
    {
        builder.ToTable("Suscriptions");
        
        builder.HasKey(s => s.Id);
        
        builder.HasOne(s => s.User)
            .WithMany(u => u.Suscriptions)
            .HasForeignKey(s => s.UserId);
        
        builder.HasOne(s => s.Plan)
            .WithMany(p => p.Suscriptions)
            .HasForeignKey(s => s.PlanId);
    }
}