using System.ComponentModel.DataAnnotations;

namespace MediLabo.Frontend.Web.Models;

public record PatientViewModel(
    int Id,
    [Required] string FirstName,
    [Required] string LastName,
    [Required] DateOnly DateOfBirth,
    [Required] char Gender,
    string? Address,
    string? PhoneNumber);