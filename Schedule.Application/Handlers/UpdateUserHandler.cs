using Microsoft.AspNetCore.Identity;
using Schedule.Domain.Entities;
using Schedule.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.DTOs.Request;

namespace Schedule.Application.Handlers
{
    public class UpdateUserHandler
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<UserAccount> _passwordHasher;

        public UpdateUserHandler(AppDbContext context, IPasswordHasher<UserAccount> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserAccount> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.Id} not found");
            }

            // Atualiza propriedades básicas
            //SetPropertyIfNotNull(user, "Name", request.Name);
            //SetPropertyIfNotNull(user, "Email", request.Email);
            // Atualiza apenas campos não nulos
    if (request.Name != null) SetProperty(user, "Name", request.Name);
    if (request.Email != null) SetProperty(user, "Email", request.Email);

            // Atualiza senha apenas se fornecida (e não vazia)
            if (request.PasswordHash != null) // Verifica se o campo foi enviado (null vs string vazia)
            {
                if (string.IsNullOrWhiteSpace(request.PasswordHash))
                {
                    throw new ArgumentException("Password cannot be empty");
                }
                user.SetPassword(request.PasswordHash, _passwordHasher);
            }

            // Atualiza demais propriedades
            SetPropertyIfNotNull(user, "BirthDate", request.BirthDate);
            SetPropertyIfNotNull(user, "Nationality", request.Nationality);
            SetPropertyIfNotNull(user, "TaxId", request.Cpf);
            SetPropertyIfNotNull(user, "PhoneNumber", request.Phone ?? request.CellPhone);
            SetPropertyIfNotNull(user, "StreetAddress", FormatAddress(request.Address, request.Number, request.Complement));
            SetPropertyIfNotNull(user, "City", request.City);
            SetPropertyIfNotNull(user, "State", request.State);
            SetPropertyIfNotNull(user, "PostalCode", request.ZipCode);
            SetPropertyIfNotNull(user, "Country", request.Naturalness);
            SetPropertyIfNotNull(user, "MaritalStatus", request.MaritalStatus);
            SetPropertyIfNotNull(user, "Gender", request.Gender);

            // Atualiza metadados
            SetProperty(user, "UpdatedAt", DateTime.UtcNow);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return user;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(request.Id))
                {
                    throw new KeyNotFoundException($"User with ID {request.Id} no longer exists");
                }
                throw;
            }
        }

        private async Task<bool> UserExists(Guid id)
        {
            return await _context.UserAccounts.AnyAsync(e => e.Id == id);
        }

        private void SetPropertyIfNotNull(UserAccount user, string propertyName, object value)
        {
            if (value != null)
            {
                SetProperty(user, propertyName, value);
            }
        }

        private void SetProperty(UserAccount user, string propertyName, object value)
        {
            var property = user.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(user, value);
            }
        }

        private string FormatAddress(string address, string number, string complement)
        {
            var formatted = $"{address}, {number}";
            if (!string.IsNullOrWhiteSpace(complement))
            {
                formatted += $" ({complement})";
            }
            return formatted;
        }
    }
}
