using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestJWTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
       private readonly List<string> _allowedExtensions= new List<string>() { ".jpg",".png"};
        private readonly long _maxAllowedPosterSize = 1048576; // 1 mig

        public MoviesController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MovieDto dto)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("onle .jpg,.png image allowed");
            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("max allowed size for poster is 1MB");


            var isValidGenre = await _context.Genres.AnyAsync(g => g.Id == dto.GenreId);
            if (!isValidGenre) return BadRequest("invalid Genre Id");
                
            using var datastream=new MemoryStream();
             await dto.Poster.CopyToAsync(datastream);


            var movie = new Movie()
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                 Poster= datastream.ToArray(),
                 Rate = dto.Rate,
                 Year = dto.Year,
                 StoreLine = dto.StoreLine,
                
            };
            await _context.Movies.AddAsync(movie);
           var result= _context.SaveChangesAsync();
            return Ok(movie);    
        }
    }
}
