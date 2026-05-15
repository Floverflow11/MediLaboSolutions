using MediLabo.Frontend.Web.Models;

namespace MediLabo.Frontend.Web.Services;

public interface IPatientService
{
    Task<IEnumerable<PatientViewModel>> GetPatientsAsync();
    Task<PatientViewModel?> GetPatientAsync(int id);
    Task UpdatePatientAsync(PatientViewModel patient);
    Task AddPatientAsync(PatientViewModel patient);
}