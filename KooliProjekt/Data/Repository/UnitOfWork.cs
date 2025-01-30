using KooliProjekt.Data.Repository;

namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            IArtistRepository todoItemRepository,
            IArtistRepository todoListRepository)
        {
            _context = context;

            TodoItemRepository = todoItemRepository;
            TodoListRepository = todoListRepository;
        }

        public IArtistRepository TodoItemRepository { get; private set; }
        public IArtistRepository TodoListRepository { get; private set; }

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
