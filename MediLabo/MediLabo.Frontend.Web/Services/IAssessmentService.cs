using MediLabo.Frontend.Web.Models;

namespace MediLabo.Frontend.Web.Services;

public interface IAssessmentService
{
    Task<AssessmentResultViewModel> GetAssessmentByPatientAsync(int patientId);
}