using KooliProjekt.Data.Repository;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class MusicTrackRepository : BaseRepository<MusicTrack>, IMusicTrackRepository
    {

        public MusicTrackRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<MusicTrack> Get(int id)
        {
            return await DbContext.Set<MusicTrack>().FindAsync(id);
        }

        public virtual async Task<PagedResult<MusicTrack>> List(int page, int pageSize, MusicTrackSearch search = null)
        {
            var query = DbContext.MusicTracks.AsQueryable();

            if (search != null)

            {
                if (!string.IsNullOrEmpty(search.Titel))
                {
                    query = query.Where(musicTrack => musicTrack.Title == search.Titel);
                }
                if (!string.IsNullOrEmpty(search.Artist))
                {
                    query = query.Where(musicTrack => musicTrack.Artist == search.Artist);
                }
                if (!string.IsNullOrEmpty(search.Year))
                {
                    query = query.Where(musicTrack => musicTrack.Year.ToString() == search.Year);
                }
                if (!string.IsNullOrEmpty(search.Pace))
                {
                    query = query.Where(musicTrack => musicTrack.Pace.ToString() == search.Pace);
                }
            }

            return await query.GetPagedAsync(page, 5);
        }

        public virtual async Task Save(MusicTrack item)
        {
            if (item.Id == 0)
            {
                DbContext.Set<MusicTrack>().Add(item);
            }
            else
            {
                DbContext.Set<MusicTrack>().Update(item);
            }

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            await DbContext.Set<MusicTrack>()
                .Where(item => item.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}