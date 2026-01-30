using Microsoft.AspNetCore.Mvc;
using Streaming.Application.DTOs.Content;
using Streaming.Application.Interfaces;
using Streaming.Domain.Entities;
using Streaming.Domain.Interfaces;

namespace Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IImagesService _imagesService;
    private readonly IUnitOfWork _unitOfWork;

    public AdminController(IImagesService imagesService, IUnitOfWork unitOfWork)
    {
        _imagesService = imagesService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("upload-content")]
    public async Task<IActionResult> UploadContent([FromForm] CreateContentRequest request)
    {
        string imageUrl = string.Empty;
        if (request.ImageFile != null)
        {
            imageUrl = await _imagesService.UploadPhotoAsync(request.ImageFile);
        }

        var newContent = new Content
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            ReleaseYear = request.ReleaseYear,
            ContentType = request.ContentType,
            UrlVideo = request.UrlVideo,
            ThumbnailUrl = imageUrl
        };

        await _unitOfWork.Contents.AddAsync(newContent);
        await _unitOfWork.SaveChangesAsync();
        
        return Ok(new { message = "Contenido subido con exito", contentId = newContent.Id });
    }
    
    [HttpPost("assign-genres")]
    public async Task<IActionResult> AssignGenres([FromBody] AssignGenresRequest request)
    {
        foreach (var genreId in request.GenreIds)
        {
            var contentGenre = new ContentGenre
            {
                ContentId = request.ContentId,
                GenreId = genreId
            };
            await _unitOfWork.ContentGenres.AddAsync(contentGenre);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(new { message = "Géneros asignados correctamente" });
    }
    
    // --- En AdminController.cs ---

    [HttpPost("create-season")]
    public async Task<IActionResult> CreateSeason([FromBody] CreateSeasonRequest request)
    {
        var season = new Season
        {
            Id = Guid.NewGuid(),
            ContentId = request.ContentId,
            SeasonNumber = request.SeasonNumber
        };

        await _unitOfWork.Seasons.AddAsync(season);
        await _unitOfWork.SaveChangesAsync();
        return Ok(new { message = "Temporada creada", seasonId = season.Id });
    }

    [HttpPost("add-episode")]
    public async Task<IActionResult> AddEpisode([FromBody] CreateEpisodeRequest request)
    {
        var episode = new Episode
        {
            Id = Guid.NewGuid(),
            SeasonId = request.SeasonId,
            Title = request.Title,
            EpisodeNumber = request.EpisodeNumber,
            DurationMinutes = request.DurationMinutes,
            UrlVideo = request.UrlVideo
        };

        await _unitOfWork.Episodes.AddAsync(episode);
        await _unitOfWork.SaveChangesAsync();
        return Ok(new { message = "Episodio añadido con éxito", episodeId = episode.Id });
    }
}