using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class DataCarrierService : IDataCarrierService
    {
        private readonly ApplicationDbContext _context;

        public DataCarrierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<DataCarrier>> List(int page, int pageSize)
        {
            return await _context.DataCarriers.GetPagedAsync(page, 5);
        }

        public async Task<DataCarrier> Get(int id)
        {
            return await _context.DataCarriers.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(DataCarrier list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var todoList = await _context.DataCarriers.FindAsync(id);
            if (todoList != null)
            {
                _context.DataCarriers.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}