namespace MediLabo.Frontend.Web.Models;

public record NoteViewModel(string Id, int PatientId, string Content, DateTime CreatedAt);