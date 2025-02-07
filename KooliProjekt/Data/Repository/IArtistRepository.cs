using KooliProjekt.Search;

namespace KooliProjekt.Data.Repository
{
    public interface IArtistRepository
    {
        Task<Artist> Get(int id);
        Task<PagedResult<Artist>> List(int page, int pageSize, ArtistSearch search = null);
        Task Save(Artist entity);
        Task Delete(int id);
    }
}
