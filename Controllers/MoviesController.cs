using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie.Models;
using MvcMovie.Data;

namespace Movie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.MovieModels == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var movies = from m in _context.MovieModels
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MovieModels == null)
            {
                return NotFound();
            }

            var movieModels = await _context.MovieModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieModels == null)
            {
                return NotFound();
            }

            return View(movieModels);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] MovieModels movieModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieModels);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MovieModels == null)
            {
                return NotFound();
            }

            var movieModels = await _context.MovieModels.FindAsync(id);
            if (movieModels == null)
            {
                return NotFound();
            }
            return View(movieModels);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] MovieModels movieModels)
        {
            if (id != movieModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieModelsExists(movieModels.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieModels);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovieModels == null)
            {
                return NotFound();
            }

            var movieModels = await _context.MovieModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieModels == null)
            {
                return NotFound();
            }

            return View(movieModels);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovieModels == null)
            {
                return Problem("Entity set 'MvcMovieContext.MovieModels'  is null.");
            }
            var movieModels = await _context.MovieModels.FindAsync(id);
            if (movieModels != null)
            {
                _context.MovieModels.Remove(movieModels);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieModelsExists(int id)
        {
          return (_context.MovieModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
