
using FP.Domain.Common;
using FP.Domain.Entities.Positions;
using System.ComponentModel.DataAnnotations;

namespace FP.Domain.Entities.Employees;

public class Employee : SoftDeletableEntity
{
    [Display(Name = "Работен номер")]
    [Required(ErrorMessage = "Работният номер е задължителен.")]
    [RegularExpression(
        @"^\d{1,4}$",
        ErrorMessage = "Работният номер трябва да съдържа от 1 до 4 цифри.")]
    public string WorkNumber { get; set; } = null!;

    [Display(Name = "Име")]
    [Required(ErrorMessage = "Името е задължително.")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "Името трябва да бъде между 2 и 100 символа.")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Презиме")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "Презимето трябва да бъде между 2 и 100 символа.")]
    public string? MiddleName { get; set; }

    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Фамилията е задължителна.")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "Фамилията трябва да бъде между 2 и 100 символа.")]
    public string LastName { get; set; } = null!;

    public int PositionId { get; set; }

    public Position Position { get; set; } = null!;

    [Display(Name = "Забележка")]
    [StringLength(
        1000,
        ErrorMessage = "Забележката не може да надвишава 1000 символа.")]
    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;
}
