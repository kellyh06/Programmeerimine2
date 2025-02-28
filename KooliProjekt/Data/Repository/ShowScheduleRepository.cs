using KooliProjekt.Data.Repository;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class ShowScheduleRepository : BaseRepository<ShowSchedule>, IShowScheduleRepository
    {

        public ShowScheduleRepository(ApplicationDbContext context) : base(context) 
        {
        }
        public override async Task<ShowSchedule> Get(int id)
        {
            return await DbContext.Set<ShowSchedule>().FindAsync(id);
        }

        public virtual async Task<PagedResult<ShowSchedule>> List(int page, int pageSize, ShowScheduleSearch search = null)
        {
            var query = DbContext.ShowSchedule.AsQueryable();

            if (search != null)
            {
                if (search.Date != null)
                {
                    var lower = search.Date.Value.Date;
                    var upper = search.Date.Value.Date.AddDays(1);
                    query = query.Where(showSchedule => showSchedule.date == search.Date);
                }
            }

            return await query.GetPagedAsync(page, 5);
        }

        public virtual async Task Save(ShowSchedule item)
        {
            if (item.Id == 0)
            {
                DbContext.Set<ShowSchedule>().Add(item);
            }
            else
            {
                DbContext.Set<ShowSchedule>().Update(item);
            }

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            await DbContext.Set<ShowSchedule>()
                .Where(item => item.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}