namespace MediLabo.Assessment.API.Models;

public record Patient(int Id, string FirstName, string LastName, DateOnly DateOfBirth, char Gender);