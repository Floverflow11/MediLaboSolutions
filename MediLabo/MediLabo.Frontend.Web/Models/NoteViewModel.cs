using System.ComponentModel.DataAnnotations;

namespace MediLabo.Frontend.Web.Models;

public record NoteViewModel(int PatientId, [Required] string Content, DateTime CreatedAt);