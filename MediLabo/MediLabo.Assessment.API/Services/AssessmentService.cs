using MediLabo.Assessment.API.Models;

namespace MediLabo.Assessment.API.Services;

public class AssessmentService : IAssessmentService
{
    private static readonly string[] TriggerWords =
    [
        "Hémoglobine A1C", "Microalbumine", "Taille", "Poids", "Fumeur", "Fumeuse",
        "Anormal", "Cholestérol", "Vertige", "Rechute", "Réaction", "Anticorps"
    ];
    
    public AssessmentResult GetResult(Patient patient, List<Note> notes)
    {
        var age = CalculateAge(patient.DateOfBirth);
        var triggerCount = GetTriggerWordCount(notes);
        var riskLevel = DetermineRiskLevel(age, patient.Gender, triggerCount);

        return new AssessmentResult(patient.Id, patient.FirstName, age, riskLevel);
    }

    private static int CalculateAge(DateOnly birth)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birth.Year;
        if (birth > today.AddYears(-age)) age--;

        if (age < 0)
            age = 0;
        
        return age;
    }

    private static int GetTriggerWordCount(List<Note> notes)
    {
        var content = string.Join(' ', notes.Select(note => note.Content));
        return TriggerWords.Count(word => content.Contains(word, StringComparison.OrdinalIgnoreCase));
    }

    private static string DetermineRiskLevel(int age, char gender, int triggerCount)
    {
        var isMale = gender is 'M' or 'm';
        var isFemale = gender is 'F' or 'f';

        switch (age)
        {
            case < 30 when isMale && triggerCount >= 5:
            case < 30 when isFemale && triggerCount >= 7:
            case >= 30 when triggerCount >= 8:
                return "Early onset";
            case < 30 when isMale && triggerCount >= 3:
            case < 30 when isFemale && triggerCount >= 4:
            case >= 30 when triggerCount >= 6:
                return "In Danger";
            case >= 30 when triggerCount >= 2:
                return "Borderline";
            default:
                return "None";
        }
    }
}