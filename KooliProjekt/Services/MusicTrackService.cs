using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class MusicTrackService : IMusicTrackService
    {
        private readonly IUnitOfWork _uow;

        public MusicTrackService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PagedResult<MusicTrack>> List(int page, int pageSize, MusicTrackSearch search = null)
        {
            return await _uow.MusicTrackRepository.List(page, pageSize, search);
        }

        public async Task<MusicTrack> Get(int? id)
        {
            return await _uow.MusicTrackRepository.Get(id.Value);
        }

        public async Task Save(MusicTrack list)
        {
            await _uow.MusicTrackRepository.Save(list);
        }

        public async Task Delete(int id)
        {
            {
                await _uow.MusicTrackRepository.Delete(id);
            }
        }
    }
}