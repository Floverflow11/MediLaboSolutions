using MediLabo.Frontend.Web.Models;
using Microsoft.AspNetCore.Mvc;
using MediLabo.Frontend.Web.Services;

namespace MediLabo.Frontend.Web.Controllers;

public class PatientController : Controller
{
    private readonly IPatientService _patientService;
    private readonly INoteService _noteService;

    public PatientController(IPatientService patientService, INoteService noteService)
    {
        _patientService = patientService;
        _noteService = noteService;
    }

    public async Task<IActionResult> Index()
    {
        var patients = await _patientService.GetPatientsAsync();
        return View(patients);
    }

    public async Task<IActionResult> Details(int id)
    {
        var patient = await _patientService.GetPatientAsync(id);

        if (patient == null)
            return NotFound();

        var notes = await _noteService.GetNotesByPatientAsync(id);
        var details = new PatientDetailsViewModel(patient, notes);

        return View(details);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var patient = await _patientService.GetPatientAsync(id);

        if (patient == null)
            return NotFound();

        return View(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PatientViewModel patient)
    {
        if (!ModelState.IsValid)
            return View(patient);

        await _patientService.UpdatePatientAsync(patient);
        return RedirectToAction("Index");
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(PatientViewModel patient)
    {
        if (!ModelState.IsValid)
            return View(patient);

        await _patientService.AddPatientAsync(patient);
        return RedirectToAction("Index");
    }

    public IActionResult AddNote(int id)
    {
        return View(new NoteViewModel(id, string.Empty, DateTime.UtcNow));
    }

    [HttpPost]
    public async Task<IActionResult> AddNote(NoteViewModel note)
    {
        if (!ModelState.IsValid)
            return View(note);

        await _noteService.AddNoteAsync(note);
        return RedirectToAction("Details", new { id = note.PatientId });
    }
}