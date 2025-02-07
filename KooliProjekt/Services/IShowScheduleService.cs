using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IShowScheduleService
    {
        Task<PagedResult<ShowSchedule>> List(int page, int pageSize, ShowScheduleSearch search = null);
        Task<ShowSchedule> Get(int? id);
        Task Save(ShowSchedule list);
        Task Delete(int id);
    }
}