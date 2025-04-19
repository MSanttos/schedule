namespace Schedule.Application.DTOs.Response
{
    public class CreateUserAccountResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public string StreetAddress { get; set; }
        public string Country { get; set; }
        public string CPF { get; set; }
    }
}
