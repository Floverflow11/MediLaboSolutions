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

    public async Task<PatientViewModel?> GetPatientAsync(int id)
    {
        var patient = await _client.GetFromJsonAsync<PatientViewModel>($"/api/patients/{id}");
        return patient;
    }

    public async Task UpdatePatientAsync(PatientViewModel patient)
    {
        var response = await _client.PutAsJsonAsync($"/api/patients/{patient.Id}", patient);
        response.EnsureSuccessStatusCode();
    }
    
    public async Task AddPatientAsync(PatientViewModel patient)
    {
        var response = await _client.PostAsJsonAsync("/api/patients", patient);
        response.EnsureSuccessStatusCode();
    }
}