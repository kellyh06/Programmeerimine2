using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _uow;

        public ArtistService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PagedResult<Artist>> List(int page, int pageSize, ArtistSearch search = null)
        {
            return await _uow.ArtistRepository.List(page, pageSize, search);
        }

        public async Task<Artist> Get(int? id)
        {
            return await _uow.ArtistRepository.Get(id.Value);
        }

        public async Task Save(Artist list)
        {
            await _uow.ArtistRepository.Save(list);
        }

        public async Task Delete(int id)
        {
            {
                await _uow.ArtistRepository.Delete(id);
            }
        }
    }
}
