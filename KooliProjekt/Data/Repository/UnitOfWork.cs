using KooliProjekt.Data.Repository;
using KooliProjekt.Services;

namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            IArtistRepository artistRepository, IMusicTrackRepository musicTrackRepository, IShowScheduleRepository showScheduleRepository)
        {
            _context = context;

            ArtistRepository = artistRepository;
            MusicTrackRepository = musicTrackRepository;
            ShowScheduleRepository = showScheduleRepository;
        }

        public IArtistRepository ArtistRepository { get; private set; }
        public IMusicTrackRepository MusicTrackRepository { get; private set; }
        public IShowScheduleRepository ShowScheduleRepository { get; private set; }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
