using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class ArtistsIndexModel
    {
        public ArtistSearch Search { get; set; }
        public PagedResult<Artist> Data { get; set; }
    }
}