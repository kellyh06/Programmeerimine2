﻿using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ShowScheduleService : IShowScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ShowScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ShowSchedule>> List(int page, int pageSize, ShowScheduleSearch search = null)
        {
            var query = _context.ShowSchedule.AsQueryable();

            if (search != null)
            {
                if (search.Date != null)
                {
                    var lower = search.Date.Value.Date;
                    var upper = search.Date.Value.Date.AddDays(1);
                    query = query.Where(showSchedule => showSchedule.date >= lower && showSchedule.date < upper);
                }
            }

            return await query.GetPagedAsync(page, 5);
        }

        public async Task<ShowSchedule> Get(int id)
        {
            return await _context.ShowSchedule.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(ShowSchedule list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var todoList = await _context.ShowSchedule.FindAsync(id);
            if (todoList != null)
            {
                _context.ShowSchedule.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}