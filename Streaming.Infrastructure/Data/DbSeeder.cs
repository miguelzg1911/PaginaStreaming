using Streaming.Domain.Entities;
using Streaming.Domain.Enums;
using Streaming.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Streaming.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Verificamos si ya existen planes para no duplicarlos
        if (!await context.Plans.AnyAsync())
        {
            await context.Plans.AddRangeAsync(
                new Plan 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "Básico", 
                    Price = 5.99m, 
                    MaxProfiles = 1, 
                    MaxResolution = "720p",
                    Description = "Ideal para ver en tu celular."
                },
                new Plan 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "Premium", 
                    Price = 12.99m, 
                    MaxProfiles = 4, 
                    MaxResolution = "4K",
                    Description = "La mejor experiencia para toda la familia."
                }
            );
        }

        // Verificamos si existen géneros
        if (!await context.Genres.AnyAsync())
        {
            await context.Genres.AddRangeAsync(
                new Genre { Id = Guid.NewGuid(), Name = "Acción" },
                new Genre { Id = Guid.NewGuid(), Name = "Terror" },
                new Genre { Id = Guid.NewGuid(), Name = "Comedia" }
            );
        }

        await context.SaveChangesAsync();
    }
}