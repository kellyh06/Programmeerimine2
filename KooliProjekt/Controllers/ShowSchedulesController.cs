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
        private readonly IShowScheduleService _showScheduleService;

        public ShowSchedulesController(IShowScheduleService showScheduleService)
        {
            _showScheduleService = showScheduleService;
        }

        // GET: ShowSchedules
        public async Task<IActionResult> Index(int page = 1, ShowSchedulesIndexModel model = null)
        {
            model = model ?? new ShowSchedulesIndexModel();
            model.Data = await _showScheduleService.List(page, 10, model.Search) ?? new PagedResult<ShowSchedule>
            {
                Results = new List<ShowSchedule>()
            };
            return View(model);
        }

        // GET: ShowSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();


            var showSchedule = await _showScheduleService.Get(id.Value);

            if (showSchedule == null) return NotFound();


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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShowSchedule showSchedule)
        {
            if (ModelState.IsValid)

            {
                await _showScheduleService.Save(showSchedule);
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

            var showSchedule = await _showScheduleService.Get(id.Value);
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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ShowSchedule showSchedule)
        {
            if (id != showSchedule.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(showSchedule);

            await _showScheduleService.Save(showSchedule);

            return RedirectToAction(nameof(Index));
        }

        // GET: ShowSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showSchedule = await _showScheduleService.Get(id.Value);
            if (showSchedule == null) return NotFound();

            return View(showSchedule);
        }


        // POST: ShowSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showSchedule = await _showScheduleService.Get(id);
            if (showSchedule != null)
            {
                await _showScheduleService.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        //private bool ShowScheduleExists(int id)
        //{
        //    return _showScheduleService.ShowSchedule.Any(e => e.Id == id);
        //}
    }
}
