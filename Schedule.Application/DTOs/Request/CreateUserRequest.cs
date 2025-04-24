using Schedule.Domain.Enums;

namespace Schedule.Application.DTOs.Request
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string Naturalness { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Rg { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CellPhone { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string CreatedBy { get; set; } // Caso você queira manter isso
    }

    public class CreateUserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = "User created successfully.";
    }
}
