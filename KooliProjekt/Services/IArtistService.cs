using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IArtistService
    {
        Task<PagedResult<Artist>> List(int page, int pageSize, ArtistSearch search = null);
        Task<Artist> Get(int? id);
        Task Save(Artist list);
        Task Delete(int id);
    }
}