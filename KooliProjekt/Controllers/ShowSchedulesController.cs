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
        private readonly IShowScheduleService _service;

        public ShowSchedulesController(IShowScheduleService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int page = 1, ShowSchedulesIndexModel model = null)
        {
            model = model ?? new ShowSchedulesIndexModel();
            model.Data = await _service.List(page, 5, model.Search);

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showSchedule = await _service.Get(id);
            if (showSchedule == null)
            {
                return NotFound();
            }

            return View(showSchedule);
        }

        public IActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,date")] ShowSchedule showSchedule)
        {
            if (ModelState.IsValid)
            {
                await _service.Save(showSchedule);
                return RedirectToAction(nameof(Index));
            }
            return View(showSchedule);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showSchedule = await _service.Get(id);
            if (showSchedule == null)
            {
                return NotFound();
            }
            return View(showSchedule);
        }

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
                await _service.Save(showSchedule);
                return RedirectToAction(nameof(Index));
            }
            return View(showSchedule);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showSchedule = await _service.Get(id);
            if (showSchedule == null)
            {
                return NotFound();
            }

            return View(showSchedule);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showSchedule = await _service.Get(id);
            if (showSchedule != null)
            {
                await _service.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
