using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Artist>> List(int page, int pageSize, ArtistSearch search = null)
        {
            var query =  _context.Artist.AsQueryable();

            if(search != null)
            {
                if(!string.IsNullOrWhiteSpace(search.Name))
                {
                    query = query.Where(artist => artist.Name.Contains(search.Name));
                }
            }

            return await query.GetPagedAsync(page, 5);
        }

        public async Task<Artist> Get(int id)
        {
            return await _context.Artist.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Artist list)
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
            var todoList = await _context.Artist.FindAsync(id);
            if (todoList != null)
            {
                _context.Artist.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
