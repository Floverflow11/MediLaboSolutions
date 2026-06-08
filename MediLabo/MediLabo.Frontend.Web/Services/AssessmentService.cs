using System.Text.Json;
using MediLabo.Frontend.Web.Models;

namespace MediLabo.Frontend.Web.Services;

public class AssessmentService : IAssessmentService
{
    private readonly HttpClient _client;

    public AssessmentService(HttpClient client)
    {
        _client = client;
    }

    public async Task<AssessmentResultViewModel> GetAssessmentByPatientAsync(int patientId)
    {
        var content =
            await _client.GetFromJsonAsync<AssessmentResultViewModel>($"/api/assessment/patient/{patientId}") ??
            throw new JsonException();
        return content;
    }
}