using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.PublicApi
{
    public interface IApiClient
    {
        Task<List<Artist>> List();
        Task Save(Artist list);
        Task Delete(int id);
    }
}