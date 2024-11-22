using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IDataCarrierService
    {
        Task<PagedResult<DataCarrier>> List(int page, int pageSize);
        Task<DataCarrier> Get(int id);
        Task Save(DataCarrier list);
        Task Delete(int id);
    }
}