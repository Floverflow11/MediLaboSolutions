using MediLabo.Frontend.Web.Models;

namespace MediLabo.Frontend.Web.Services;

public class PatientService : IPatientService
{
    private readonly HttpClient _client;

    public PatientService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<PatientViewModel>> GetPatientsAsync()
    {
        var content = await _client.GetFromJsonAsync<IEnumerable<PatientViewModel>>("/api/patients");
        return content ?? [];
    }
}