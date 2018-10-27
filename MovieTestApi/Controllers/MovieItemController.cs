using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MovieTestApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieItemController : ControllerBase
    {
        private readonly MovieContext _context;

        public MovieItemController(MovieContext context)
        {
            _context = context;

            if (_context.MovieItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.MovieItems.Add(new MovieItem { Name = "Movie1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<MovieItem>> GetAll()
        {
            return _context.MovieItems.ToList();
        }

        [HttpGet("{id}", Name = "GetMovies")]
        public ActionResult<MovieItem> GetById(long id)
        {
            var mv = _context.MovieItems.Find(id);
            if (mv == null)
            {
                return NotFound();
            }
            return mv;
        }

        [HttpPost]
        public IActionResult Create(MovieItem mv)
        {
            _context.MovieItems.Add(mv);
            _context.SaveChanges();

            return CreatedAtRoute("GetMovie", new { id = mv.Id }, mv);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, MovieItem mv)
        {
            var mov = _context.MovieItems.Find(id);
            if (mov == null)
            {
                return NotFound();
            }

            mov.IsComplete = mv.IsComplete;
            mov.Path = mv.Path;
            mov.StartAt = mv.StartAt;
            mov.Name = mv.Name;

            _context.MovieItems.Update(mov);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var mv = _context.MovieItems.Find(id);
            if (mv == null)
            {
                return NotFound();
            }

            _context.MovieItems.Remove(mv);
            _context.SaveChanges();
            return NoContent();
        }
    }
}