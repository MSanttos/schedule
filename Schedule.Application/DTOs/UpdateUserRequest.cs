namespace Schedule.Application.DTOs
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Lembre-se de fazer o hash da senha aqui
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
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
    }
}
