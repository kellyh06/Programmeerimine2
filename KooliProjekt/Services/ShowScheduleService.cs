using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Data.Repository;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ShowScheduleService :  IShowScheduleService
    {

        private readonly IUnitOfWork _uow;

        public ShowScheduleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PagedResult<ShowSchedule>> List(int page, int pageSize, ShowScheduleSearch search = null)
        {
            return await _uow.ShowScheduleRepository.List(page, pageSize, search);
            
        }

        public async Task<ShowSchedule> Get(int? id)
        {
            return await _uow.ShowScheduleRepository.Get(id.Value);
        }

        public async Task Save(ShowSchedule list)
        {
            await _uow.ShowScheduleRepository.Save(list);
        }

        public async Task Delete(int id)
        {
            {
                await _uow.ShowScheduleRepository.Delete(id);
            }
        }
    }
}