namespace KooliProjekt.Data.Repository
{
    public interface IArtistRepository
    {
        Task<Artist> Get(int id);
        Task<PagedResult<Artist>> List(int page, int pageSize);
    }
}
