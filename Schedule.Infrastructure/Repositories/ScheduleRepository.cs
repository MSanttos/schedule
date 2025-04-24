using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces;
using Schedule.Infrastructure.Persistence;

namespace Schedule.Infrastructure.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDbContext _context;

        public ScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Schedulers Schedule, CancellationToken cancellationToken)
        {
            await _context.Schedulers.AddAsync(Schedule, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Schedulers>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Schedulers?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Schedulers Schedule, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}