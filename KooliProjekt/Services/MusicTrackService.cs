using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class MusicTrackService : IMusicTrackService
    {
        private readonly ApplicationDbContext _context;

        public MusicTrackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<MusicTrack>> List(int page, int pageSize)
        {
            return await _context.MusicTracks.GetPagedAsync(page, 5);
        }

        public async Task<MusicTrack> Get(int id)
        {
            return await _context.MusicTracks.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(MusicTrack list)
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
            var todoList = await _context.MusicTracks.FindAsync(id);
            if (todoList != null)
            {
                _context.MusicTracks.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}