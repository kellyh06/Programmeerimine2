using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IMusicTrackService
    {
        Task<PagedResult<MusicTrack>> List(int page, int pageSize);
        Task<MusicTrack> Get(int id);
        Task Save(MusicTrack list);
        Task Delete(int id);
    }
}