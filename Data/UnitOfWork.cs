using Core.Repositories;
using Data.Repositories;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly FootballManagerDbContext _context;
        private TournamentRepository _tournamentRepository;

        public UnitOfWork(FootballManagerDbContext context)
        {
            _context = context;
        }

        public ITournamentRepository Tournament
            => _tournamentRepository = _tournamentRepository ?? new TournamentRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
