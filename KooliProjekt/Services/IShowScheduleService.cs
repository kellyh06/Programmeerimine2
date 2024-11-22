using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IShowScheduleService
    {
        Task<PagedResult<ShowSchedule>> List(int page, int pageSize);
        Task<ShowSchedule> Get(int id);
        Task Save(ShowSchedule list);
        Task Delete(int id);
    }
}