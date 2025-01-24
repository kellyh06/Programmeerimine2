using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Models;

namespace KooliProjekt.Controllers
{
    public class ShowSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IShowScheduleService _showScheduleService;

        public ShowSchedulesController(IShowScheduleService showScheduleService, ApplicationDbContext context)
        {
            _showScheduleService = showScheduleService;
            _context = context;
        }

        // GET: ShowSchedules
        public async Task<IActionResult> Index(int page = 1, ShowSchedulesIndexModel model = null)
        {
            model = model ?? new ShowSchedulesIndexModel();
            model.Data = await _showScheduleService.List(page, 10, model.Search);

            return View(model);
        }

        // GET: ShowSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showSchedule = await _context.ShowSchedule
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showSchedule == null)
            {
                return NotFound();
            }

            return View(showSchedule);
        }

        // GET: ShowSchedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShowSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,date")] ShowSchedule showSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(showSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(showSchedule);
        }

        // GET: ShowSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showSchedule = await _context.ShowSchedule.FindAsync(id);
            if (showSchedule == null)
            {
                return NotFound();
            }
            return View(showSchedule);
        }

        // POST: ShowSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,date")] ShowSchedule showSchedule)
        {
            if (id != showSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(showSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowScheduleExists(showSchedule.Id))
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
            return View(showSchedule);
        }

        // GET: ShowSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showSchedule = await _context.ShowSchedule
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showSchedule == null)
            {
                return NotFound();
            }

            return View(showSchedule);
        }

        // POST: ShowSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showSchedule = await _context.ShowSchedule.FindAsync(id);
            if (showSchedule != null)
            {
                _context.ShowSchedule.Remove(showSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowScheduleExists(int id)
        {
            return _context.ShowSchedule.Any(e => e.Id == id);
        }
    }
}
