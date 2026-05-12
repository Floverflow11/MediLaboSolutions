using MediLabo.Frontend.Web.Models;

namespace MediLabo.Frontend.Web.Services;

public interface IPatientService
{
    Task<IEnumerable<PatientViewModel>> GetPatientsAsync();
}