using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IDataCarrierMusicService
    {
        Task<PagedResult<DataCarrierMusic>> List(int page, int pageSize);
        Task<DataCarrierMusic> Get(int id);
        Task Save(DataCarrierMusic list);
        Task Delete(int id);
    }
}