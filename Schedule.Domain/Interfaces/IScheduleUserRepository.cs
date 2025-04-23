using Schedule.Domain.Entities;

namespace Schedule.Domain.Interfaces
{
    public interface IScheduleRepository
    {
        Task AddAsync(Schedulers Schedule, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        Task<Schedulers?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Schedulers>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(Schedulers Schedule, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}



/*
    Task AddAsync(UserAccount user, CancellationToken cancellationToken);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
    Task<UserAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(UserAccount user, CancellationToken cancellationToken);
 */