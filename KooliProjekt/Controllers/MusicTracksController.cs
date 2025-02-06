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
    public class MusicTracks : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IMusicTrackService _service;

        public MusicTracks(IMusicTrackService service)
        {
            _service = service;
        }

        // GET: MusicTracks
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await _service.List(page, 5));
        }

        // GET: MusicTracks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicTrack = await _service.Get(id);
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
                await _service.Save(musicTrack);
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

            var musicTrack = await _service.Get(id);
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
                await _service.Save(musicTrack);
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

            var musicTrack = await _service.Get(id);
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
            var musicTrack = await _service.Get(id);
            if (musicTrack != null)
            {
                await _service.Delete(musicTrack.Id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
