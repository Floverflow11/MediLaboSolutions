using Microsoft.AspNetCore.Mvc;
using MediLabo.Frontend.Web.Services;

namespace MediLabo.Frontend.Web.Controllers;

public class PatientController : Controller
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
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

        return View(patient);
    }
}