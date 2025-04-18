using Microsoft.AspNetCore.Identity;
using Schedule.Application.DTOs;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces;

namespace Schedule.Application.Handlers
{
    public class CreateUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserAccount> _passwordHasher;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<UserAccount>(); // Injetável se quiser
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
                throw new InvalidOperationException("Email already registered.");

            var user = UserAccount.Create(
                request.Name,
                request.Email,
                request.BirthDate,
                request.Nationality,
                request.Naturalness,
                request.Cpf,
                request.Rg,
                request.Phone,
                request.CellPhone,
                request.ZipCode,
                request.Address,
                request.Number,
                request.Complement,
                request.Neighborhood,
                request.City,
                request.State,
                request.Gender,
                request.MaritalStatus
            );

            user.SetPassword(request.Password, _passwordHasher);
            await _userRepository.AddAsync(user, cancellationToken);

            return new CreateUserResponse
            {
                Id = user.Id,
                Email = user.Email
            };
        }
    }
}
