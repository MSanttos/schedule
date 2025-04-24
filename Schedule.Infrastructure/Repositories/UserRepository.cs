using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces;
using Schedule.Infrastructure.Persistence;

namespace Schedule.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserAccount user, CancellationToken cancellationToken)
        {
            _context.UserAccounts.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.UserAccounts.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<UserAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<UserAccount>().FindAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(UserAccount user, CancellationToken cancellationToken)
        {
            _context.UserAccounts.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
