using Schedule.Domain.Enums;

namespace Schedule.Application.DTOs.Request
{
    public class UpdateUserRequest
    {
        public Guid Id { get; set; }  // Adicionando o ID que é necessário para updates
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; } // Lembre-se de fazer o hash da senha aqui
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Naturalness { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
    }
}
