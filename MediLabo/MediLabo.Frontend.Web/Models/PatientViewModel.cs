namespace MediLabo.Frontend.Web.Models;

public record PatientViewModel(
    int Id,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    char Gender,
    string? Address,
    string? PhoneNumber);