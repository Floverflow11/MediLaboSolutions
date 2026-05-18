using MediLabo.Frontend.Web.Models;

namespace MediLabo.Frontend.Web.Services;

public class NoteService : INoteService
{
    private readonly HttpClient _client;

    public NoteService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<NoteViewModel>> GetNotesByPatientAsync(int id)
    {
        var content = await _client.GetFromJsonAsync<IEnumerable<NoteViewModel>>($"/api/notes/patient/{id}");
        return content ?? [];
    }
}