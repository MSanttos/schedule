using Schedule.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Schedule.Domain.Entities
{
    public class UserAccount
    {
        // Identificação
        public Guid Id { get; private set; } = Guid.NewGuid();

        // Dados básicos
        private string _name;
        public string Name
        {
            get => _name;
            private set => _name = ValidateName(value);
        }

        // Dados de contato
        private string _email;
        [EmailAddress]
        public string Email
        {
            get => _email;
            private set => _email = ValidateEmail(value);
        }

        public string? PhoneNumber { get; private set; }
        public bool PhoneNumberVerified { get; private set; }
        public bool EmailVerified { get; private set; }

        // Segurança
        public string PasswordHash { get; private set; }
        public DateTime? PasswordLastChanged { get; private set; }
        public string? PasswordResetToken { get; private set; }
        public DateTime? PasswordResetTokenExpires { get; private set; }

        // Dados demográficos
        public DateTime? BirthDate { get; private set; }
        public MaritalStatus? MaritalStatus { get; private set; }
        public Gender? Gender { get; private set; }
        public string? Nationality { get; private set; }
        public string? TaxId { get; private set; }

        // Endereço
        public string? StreetAddress { get; private set; }
        public string? City { get; private set; }
        public string? State { get; private set; }
        public string? PostalCode { get; private set; }
        public string? Country { get; private set; }

        // Controle
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }
        public string? CreatedBy { get; private set; }
        public string? UpdatedBy { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsDeleted { get; private set; } = false;
        public DateTime? DeletedAt { get; private set; }
        public string? DeletedBy { get; private set; }

        // Construtores
        protected UserAccount() { } // Para ORM

        public UserAccount(string name, string email, string? createdBy = null)
        {
            Name = name;
            Email = email;
            CreatedBy = createdBy;
        }

        // Métodos de segurança
        public void SetPassword(string password, IPasswordHasher<UserAccount> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.");

            PasswordHash = passwordHasher.HashPassword(this, password);
            PasswordLastChanged = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RequestPasswordReset()
        {
            PasswordResetToken = Guid.NewGuid().ToString();
            PasswordResetTokenExpires = DateTime.UtcNow.AddHours(2);
            UpdatedAt = DateTime.UtcNow;
        }

        // Métodos de status
        public void Activate(string activatedBy = null)
        {
            IsActive = true;
            IsDeleted = false;
            DeletedAt = null;
            DeletedBy = null;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = activatedBy;
        }

        public void Deactivate(string deactivatedBy = null)
        {
            IsActive = false;
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = deactivatedBy;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = deactivatedBy;
        }

        // Métodos de fábrica
        public static UserAccount Create(
            string name,
            string email,
            DateTime? birthDate,
            string? nationality,
            string? naturalness,
            string? cpf,
            string? rg,
            string? phone,
            string? cellPhone,
            string? zipCode,
            string? address,
            string? number,
            string? complement,
            string? neighborhood,
            string? city,
            string? state,
            Gender? gender,
            MaritalStatus? maritalStatus,
            string? createdBy = null)
        {
            return new UserAccount(name, email, createdBy)
            {
                BirthDate = birthDate,
                Nationality = nationality,
                TaxId = cpf,
                StreetAddress = address,
                City = city,
                State = state,
                PostalCode = zipCode,
                Country = naturalness,
                PhoneNumber = phone,
                PhoneNumberVerified = false,
                EmailVerified = false,
                MaritalStatus = maritalStatus,
                Gender = gender
            };
        }

        // Métodos de validação
        private static string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty or whitespace.");

            if (name.Length > 100)
                throw new ArgumentException("Name cannot be longer than 100 characters.");

            return name.Trim();
        }

        private static string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format.");

            return email.ToLower().Trim();
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                return new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Métodos adicionais
        public void VerifyEmail()
        {
            EmailVerified = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void VerifyPhone()
        {
            PhoneNumberVerified = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}