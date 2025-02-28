using KooliProjekt.Data.Repository;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Artist> Get(int id)
        {
            return await DbContext.Set<Artist>().FindAsync(id);
        }

        public virtual async Task<PagedResult<Artist>> List(int page, int pageSize, ArtistSearch search = null)
        {
            var query = DbContext.Artists.AsQueryable();

            if (search != null)
            {
                if (search.Name != null)
                {
                    query = query.Where(artist => artist.Name == search.Name);
                }
            }

            return await query.GetPagedAsync(page, 5);
        }

        public virtual async Task Save(Artist item)
        {
            if (item.Id == 0)
            {
                DbContext.Set<Artist>().Add(item);
            }
            else
            {
                DbContext.Set<Artist>().Update(item);
            }

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            await DbContext.Set<Artist>()
                .Where(item => item.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}