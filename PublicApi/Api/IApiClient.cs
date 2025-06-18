using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.PublicApi
{
    public interface IApiClient
    {
        Task<Result<List<Artist>>> List();
        Task<Result> Save(Artist artist);
        Task<Result> Delete(int id);
    }
}
