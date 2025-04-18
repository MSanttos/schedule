using System.ComponentModel.DataAnnotations;

namespace Schedule.Domain.Enums
{
    /// <summary>
    /// Representa o estado civil de uma pessoa
    /// </summary>
    public enum MaritalStatus
    {
        /// <summary>
        /// Solteiro(a)
        /// </summary>
        [Display(Name = "Single", Description = "Never married")]
        Single = 1,

        /// <summary>
        /// Casado(a)
        /// </summary>
        [Display(Name = "Married", Description = "Legally married")]
        Married = 2,

        /// <summary>
        /// Divorciado(a)
        /// </summary>
        [Display(Name = "Divorced", Description = "Marriage legally dissolved")]
        Divorced = 3,

        /// <summary>
        /// Viúvo(a)
        /// </summary>
        [Display(Name = "Widowed", Description = "Spouse has died")]
        Widowed = 4,

        /// <summary>
        /// União Estável
        /// </summary>
        [Display(Name = "Domestic Partnership", Description = "In a committed relationship but not legally married")]
        DomesticPartnership = 5,

        /// <summary>
        /// Separado(a)
        /// </summary>
        [Display(Name = "Separated", Description = "Married but living apart")]
        Separated = 6,

        /// <summary>
        /// Casado(a) com Regime de Separação
        /// </summary>
        [Display(Name = "Legally Separated", Description = "Marriage legally recognized but under separation")]
        LegallySeparated = 7,

        /// <summary>
        /// Casado(a) com Regime de Bens
        /// </summary>
        [Display(Name = "Married with Prenup", Description = "Married with prenuptial agreement")]
        MarriedWithPrenup = 8,

        /// <summary>
        /// Outro (especificar)
        /// </summary>
        [Display(Name = "Other", Description = "Other marital status")]
        Other = 99
    }
}
