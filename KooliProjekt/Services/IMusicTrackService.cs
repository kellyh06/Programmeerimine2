using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IMusicTrackService
    {
        Task<PagedResult<MusicTrack>> List(int page, int pageSize, MusicTrackSearch search);
        Task<MusicTrack> Get(int? id);
        Task Save(MusicTrack list);
        Task Delete(int id);
    }
}