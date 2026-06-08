using MediLabo.Assessment.API.Models;

namespace MediLabo.Assessment.API.Services;

public interface IAssessmentService
{
    public AssessmentResult GetResult(Patient patient, List<Note> notes);
}