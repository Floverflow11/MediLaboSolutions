namespace MediLabo.Assessment.API.Models;

public record AssessmentResult(int PatientId, string PatientName, int PatientAge, string PatientRiskLevel);