using MediLabo.Frontend.Web.Models;

namespace MediLabo.Frontend.Web.Services;

public interface INoteService
{
    Task<IEnumerable<NoteViewModel>> GetNotesByPatientAsync(int id);
}