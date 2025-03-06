using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ITB2203Application.Controllers
{
    public class MovieController : Controller
    {
        private readonly DataContext context;
        public MovieController(DataContext c)
        {
            context = c;
        }
        [HttpGet]
        public IActionResult GetMovies()
        {
            var movies = context.MovieList!.AsQueryable();
            return Ok(movies);

        }
        [HttpPost]
        public IActionResult CreateMovie([FromBody] Movie e)
        {
            var dbMovie = context.MovieList?.Find(e.Id);
            if (dbMovie == null)
            {
                context.MovieList?.Add(e);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetMovies), new { e.Id }, e);
            }
            return Conflict();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int? id, [FromBody] Movie e)
        {
            var dbMovie = context.MovieList!.AsNoTracking().FirstOrDefault(movieInDB => movieInDB.Id == e.Id);
            if (id != e.Id || dbMovie == null) return NotFound();
            context.Update(e);
            context.SaveChanges();
            return NoContent();
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movieToDelete = context.MovieList?.Find(id);
            if (movieToDelete == null) return NotFound();
            context.MovieList?.Remove(movieToDelete);
            context.SaveChanges();
            return NoContent();
        }

    }
}
