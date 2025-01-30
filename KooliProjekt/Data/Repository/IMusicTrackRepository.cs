namespace KooliProjekt.Data.Repository
{
    public interface IMusicTrackRepository
    {
        Task<MusicTrack> Get(int id);
        Task<PagedResult<MusicTrack>> List(int page, int pageSize);
    }
}