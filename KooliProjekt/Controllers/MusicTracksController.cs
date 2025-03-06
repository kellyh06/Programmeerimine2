using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Services;

namespace KooliProjekt.Controllers
{
    public class MusicTracksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMusicTrackService _musicTrackService;

        public MusicTracksController(ApplicationDbContext context, IMusicTrackService musicTrackService)
        {
            _context = context;
            _musicTrackService = musicTrackService;
        }

        // GET: MusicTracks
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await _context.MusicTracks.GetPagedAsync(page, 10));
        }

        // GET: MusicTracks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicTrack = await _context.MusicTracks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicTrack == null)
            {
                return NotFound();
            }

            return View(musicTrack);
        }

        // GET: MusicTracks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MusicTracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Artist,Year,Pace")] MusicTrack musicTrack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musicTrack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musicTrack);
        }

        // GET: MusicTracks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicTrack = await _context.MusicTracks.FindAsync(id);
            if (musicTrack == null)
            {
                return NotFound();
            }
            return View(musicTrack);
        }

        // POST: MusicTracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Artist,Year,Pace")] MusicTrack musicTrack)
        {
            if (id != musicTrack.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicTrack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicTrackExists(musicTrack.Id))
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
            return View(musicTrack);
        }

        // GET: MusicTracks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicTrack = await _context.MusicTracks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicTrack == null)
            {
                return NotFound();
            }

            return View(musicTrack);
        }

        // POST: MusicTracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicTrack = await _context.MusicTracks.FindAsync(id);
            if (musicTrack != null)
            {
                _context.MusicTracks.Remove(musicTrack);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool MusicTrackExists(int id)
        {
            return _context.MusicTracks.Any(e => e.Id == id);
        }
    }
}
