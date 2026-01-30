using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streaming.Domain.Entities;
using Streaming.Domain.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // Solo el admin crea géneros
public class GenreController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public GenreController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string name)
    {
        var genre = new Genre { Id = Guid.NewGuid(), Name = name };
        await _unitOfWork.Genres.AddAsync(genre);
        await _unitOfWork.SaveChangesAsync();
        return Ok(genre);
    }

    [HttpGet]
    [AllowAnonymous] // Cualquiera puede ver los géneros para navegar
    public async Task<IActionResult> GetAll()
    {
        var genres = await _unitOfWork.Genres.GetAllAsync();
        return Ok(genres);
    }
}