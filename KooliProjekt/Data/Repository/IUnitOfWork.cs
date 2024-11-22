using KooliProjekt.Data.Repository;

namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();

        IArtistRepository ArtistRepository { get; }
    }
}