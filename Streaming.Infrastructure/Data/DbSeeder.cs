using Streaming.Domain.Entities;
using Streaming.Domain.Enums;
using Streaming.Infrastructure.Data;

namespace Streaming.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Plans.Any())
        {
            var planId = Guid.NewGuid();
            await context.Plans.AddAsync(new Plan {
                Id = planId,
                Name = "Premium",
                Price = 9.99m,
                MaxProfiles = 4,
                MaxResolution = "4K",
                Description = "El mejor plan para toda la familia"
            });

            var genre = new Genre { Id = Guid.NewGuid(), Name = "Acción" };
            await context.Genres.AddAsync(genre);

            var movie = new Content {
                Id = Guid.NewGuid(),
                Title = "Película de Prueba",
                Description = "Una peli increíble",
                ContentType = ContentType.Movie,
                UrlVideo = "https://vimeo.com/76979871", // Video de ejemplo
                ThumbnailUrl = "https://res.cloudinary.com/demo/image/upload/sample.jpg"
            };
            await context.Contents.AddAsync(movie);
            
            await context.SaveChangesAsync();
        }
    }
}