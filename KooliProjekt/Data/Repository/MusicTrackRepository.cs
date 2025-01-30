﻿using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class MusicTrackRepository<T> where T : Entity
    {
        protected ApplicationDbContext DbContext { get; }

        public MusicTrackRepository(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public virtual async Task<T> Get(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<PagedResult<T>> List(int page, int pageSize)
        {
            return await DbContext.Set<T>()
                .OrderByDescending(x => x.Id)
                .GetPagedAsync(page, pageSize);
        }

        public virtual async Task Save(T item)
        {
            if (item.Id == 0)
            {
                DbContext.Set<T>().Add(item);
            }
            else
            {
                DbContext.Set<T>().Update(item);
            }

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            await DbContext.Set<T>()
                .Where(item => item.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}