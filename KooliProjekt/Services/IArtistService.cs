using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IArtistService
    {
        Task<PagedResult<Artist>> List(int page, int pageSize);
        Task<Artist> Get(int id);
        Task Save(Artist list);
        Task Delete(int id);
    }
}