using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace KooliProjekt.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return;
            }
           
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
        }

        public async Task<Artist> Get(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public async Task<PagedResult<Artist>> List(int page, int pageSize, ArtistSearch search = null)
        {
            var query = _context.Artists.AsQueryable();

            if (search != null)
            {
                if(!string.IsNullOrWhiteSpace(search.Keyword))
                {
                    search.Keyword = search.Keyword.Trim();

                    query = query.Where(list => 
                        list.Name.Contains(search.Keyword)
                    );
                }
            }

            return await query
                .OrderBy(list => list.Name)
                .GetPagedAsync(page, pageSize);
        }

        public async Task Save(Artist list)
        {
            if(list.Id == 0)
            {
                _context.Artists.Add(list);
            }
            else
            {
                _context.Artists.Update(list);
            }

            await _context.SaveChangesAsync();
        }
    }
}