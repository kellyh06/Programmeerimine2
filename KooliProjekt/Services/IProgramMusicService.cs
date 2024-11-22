using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IProgramMusicService
    {
        Task<PagedResult<ProgramMusic>> List(int page, int pageSize);
        Task<ProgramMusic> Get(int id);
        Task Save(ProgramMusic list);
        Task Delete(int id);
    }
}