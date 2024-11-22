using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ProgramMusicService : IProgramMusicService
    {
        private readonly ApplicationDbContext _context;

        public ProgramMusicService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ProgramMusic>> List(int page, int pageSize)
        {
            return await _context.ProgramMusics.GetPagedAsync(page, 5);
        }

        public async Task<ProgramMusic> Get(int id)
        {
            return await _context.ProgramMusics.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(ProgramMusic list)
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
            var todoList = await _context.ProgramMusics.FindAsync(id);
            if (todoList != null)
            {
                _context.ProgramMusics.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
