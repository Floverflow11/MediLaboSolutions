namespace MediLabo.Patient.Domain;

public class Patient
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public char Gender { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}