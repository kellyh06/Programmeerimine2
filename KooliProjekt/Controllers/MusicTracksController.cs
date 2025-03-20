using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class MusicTracksController : Controller
    {
        private readonly IMusicTrackService _musicTrackService;
        private readonly ILogger<MusicTracksController> _logger;

        public MusicTracksController(IMusicTrackService musicTrackService, ILogger<MusicTracksController> logger)
        {
            _musicTrackService = musicTrackService;
            _logger = logger;
        }

        // GET: MusicTracks
        public async Task<IActionResult> Index(int page = 1, MusicTracksIndexModel model = null)
        {
            model ??= new MusicTracksIndexModel();

            try
            {
                model.Data = await _musicTrackService.List(page, 10, model.Search) ?? new PagedResult<MusicTrack>
                {
                    Results = new List<MusicTrack>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Viga muusikapalade loendi laadimisel.");
                ModelState.AddModelError("", "Tekkis viga muusikapalade laadimisel.");
                model.Data = new PagedResult<MusicTrack> { Results = new List<MusicTrack>() };
            }

            return View(model);
        }

        // GET: MusicTracks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var musicTrack = await _musicTrackService.Get(id.Value);
            if (musicTrack == null) return NotFound();

            return View(musicTrack);
        }

        // GET: MusicTracks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MusicTracks/Create
        [HttpPost]
        public async Task<IActionResult> Create(MusicTrack musicTrack)
        {
            if (!ModelState.IsValid) return View(musicTrack);

            try
            {
                await _musicTrackService.Save(musicTrack);
                _logger.LogInformation("Lisati uus muusikapala: {Title}", musicTrack.Title);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Muusikapala loomine ebaõnnestus.");
                ModelState.AddModelError("", "Muusikapala lisamine ebaõnnestus.");
                return View(musicTrack);
            }
        }

        // GET: MusicTracks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var musicTrack = await _musicTrackService.Get(id.Value);
            if (musicTrack == null) return NotFound();

            return View(musicTrack);
        }

        // POST: MusicTracks/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, MusicTrack musicTrack)
        {
            if (id != musicTrack.Id) return NotFound();
            if (!ModelState.IsValid) return View(musicTrack);

            try
            {
                await _musicTrackService.Save(musicTrack);
                _logger.LogInformation("Muusikapala muudetud: {Title}", musicTrack.Title);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Muusikapala muutmine ebaõnnestus.");
                ModelState.AddModelError("", "Muusikapala muutmine ebaõnnestus.");
                return View(musicTrack);
            }
        }

        // GET: MusicTracks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var musicTrack = await _musicTrackService.Get(id.Value);
            if (musicTrack == null) return NotFound();

            return View(musicTrack);
        }

        // POST: MusicTracks/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicTrack = await _musicTrackService.Get(id);
            if (musicTrack != null)
            {
                await _musicTrackService.Delete(id);
                _logger.LogInformation("Muusikapala kustutatud: {Title}", musicTrack.Title);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
