using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameLiterature.Data;
using GameLiterature.Models;

namespace GameLiterature.Controllers
{
    public class LiteraturesController : Controller
    {
        private readonly GameLiteratureDbContext _context;

        public LiteraturesController(GameLiteratureDbContext context)
        {
            _context = context;
        }

        // GET: Literatures
        public async Task<IActionResult> Index()
        {
              return View(await _context.Literature.ToListAsync());
        }

        // GET: Literatures/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // Post: Literatures/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index",await _context.Literature.Where(x => x.LiteratureQuestion.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Literatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Literature == null)
            {
                return NotFound();
            }

            var literature = await _context.Literature
                .FirstOrDefaultAsync(m => m.Id == id);
            if (literature == null)
            {
                return NotFound();
            }

            return View(literature);
        }

        // GET: Literatures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Literatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LiteratureQuestion,LiteratureAnswer")] Literature literature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(literature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(literature);
        }

        // GET: Literatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Literature == null)
            {
                return NotFound();
            }

            var literature = await _context.Literature.FindAsync(id);
            if (literature == null)
            {
                return NotFound();
            }
            return View(literature);
        }

        // POST: Literatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LiteratureQuestion,LiteratureAnswer")] Literature literature)
        {
            if (id != literature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(literature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LiteratureExists(literature.Id))
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
            return View(literature);
        }

        // GET: Literatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Literature == null)
            {
                return NotFound();
            }

            var literature = await _context.Literature
                .FirstOrDefaultAsync(m => m.Id == id);
            if (literature == null)
            {
                return NotFound();
            }

            return View(literature);
        }

        // POST: Literatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Literature == null)
            {
                return Problem("Entity set 'GameLiteratureDbContext.Literature'  is null.");
            }
            var literature = await _context.Literature.FindAsync(id);
            if (literature != null)
            {
                _context.Literature.Remove(literature);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LiteratureExists(int id)
        {
          return _context.Literature.Any(e => e.Id == id);
        }
    }
}
