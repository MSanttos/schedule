using System.ComponentModel.DataAnnotations;

namespace Schedule.Domain.Enums
{
    /// <summary>
    /// Representa a identidade de gênero de uma pessoa
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Masculino
        /// </summary>
        [Display(Name = "Male", Description = "Male gender identity")]
        Male = 1,

        /// <summary>
        /// Feminino
        /// </summary>
        [Display(Name = "Female", Description = "Female gender identity")]
        Female = 2,

        /// <summary>
        /// Não-binário
        /// </summary>
        [Display(Name = "Non-binary", Description = "Does not identify exclusively as male or female")]
        NonBinary = 3,

        /// <summary>
        /// Transgênero
        /// </summary>
        [Display(Name = "Transgender", Description = "Gender identity differs from sex assigned at birth")]
        Transgender = 4,

        /// <summary>
        /// Gênero Fluido
        /// </summary>
        [Display(Name = "Genderfluid", Description = "Gender identity is not fixed")]
        Genderfluid = 5,

        /// <summary>
        /// Agênero
        /// </summary>
        [Display(Name = "Agender", Description = "Does not identify with any gender")]
        Agender = 6,

        /// <summary>
        /// Dois-espíritos (indígena norte-americano)
        /// </summary>
        [Display(Name = "Two-Spirit", Description = "Indigenous North American gender variant identity")]
        TwoSpirit = 7,

        /// <summary>
        /// Prefiro não informar
        /// </summary>
        [Display(Name = "Prefer not to say", Description = "Declines to disclose gender identity")]
        PreferNotToSay = 98,

        /// <summary>
        /// Outro (especificar)
        /// </summary>
        [Display(Name = "Other", Description = "Gender identity not listed")]
        Other = 99
    }
}
