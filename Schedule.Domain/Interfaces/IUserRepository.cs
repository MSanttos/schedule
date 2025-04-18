using Schedule.Domain.Entities;

namespace Schedule.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(UserAccount user, CancellationToken cancellationToken);
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
        Task<UserAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(UserAccount user, CancellationToken cancellationToken);
    }
}
