namespace MediLabo.Frontend.Web.Models;

public record PatientDetailsViewModel(PatientViewModel Patient, IEnumerable<NoteViewModel> Notes, AssessmentResultViewModel Assessment);