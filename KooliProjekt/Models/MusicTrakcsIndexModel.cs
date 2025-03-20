using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class MusicTracksIndexModel
    {
        public MusicTrackSearch Search { get; set; }
        public PagedResult<MusicTrack> Data { get; set; }
    }
}
