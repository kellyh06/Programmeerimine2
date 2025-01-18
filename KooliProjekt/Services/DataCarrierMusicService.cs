using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class DataCarrierMusicService : IDataCarrierMusicService
    {
        private readonly ApplicationDbContext _context;

        public DataCarrierMusicService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<DataCarrierMusic>> List(int page, int pageSize)
        {
            return await _context.DataCarrierMusics.GetPagedAsync(page, 5);
        }

        public async Task<DataCarrierMusic> Get(int id)
        {
            return await _context.DataCarrierMusics.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(DataCarrierMusic list)
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
            var todoList = await _context.DataCarrierMusics.FindAsync(id);
            if (todoList != null)
            {
                _context.DataCarrierMusics.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}