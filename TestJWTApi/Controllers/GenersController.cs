using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestJWTApi.Models;

namespace TestJWTApi.Controllers
{

   
    [Route("api/[controller]")]
    [ApiController]
    public class GenersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public GenersController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var geners = await _context.Genres.OrderBy(g => g.Name).ToListAsync();   
            return Ok(geners);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateGenereDto dto)
        {
            var genre = new Genre
            {
                Name=dto.Name
            };
            await _context.AddAsync(genre);
            var result=_context.SaveChanges();
            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromBody]CreateGenereDto dto)
        {
            var genre= await _context.Genres.SingleOrDefaultAsync(g => g.Id==id);

            if (genre == null) return NotFound($"No Genre was found with id :{id}");

            genre.Name = dto.Name;
            _context.SaveChanges();
            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre =await _context.Genres.SingleOrDefaultAsync(gen => gen.Id==id);
            if (genre == null) return NotFound($"No Genre was found with id :{id}");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return Ok(genre);   
        }
    }
}
