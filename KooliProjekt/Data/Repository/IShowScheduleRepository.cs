using KooliProjekt.Search;

namespace KooliProjekt.Data.Repository
{
    public interface IShowScheduleRepository
    {
        Task<ShowSchedule> Get(int id);
        Task<PagedResult<ShowSchedule>> List(int page, int pageSize, ShowScheduleSearch search = null);
        Task Save(ShowSchedule entity);
        Task Delete(int id);
    }
}