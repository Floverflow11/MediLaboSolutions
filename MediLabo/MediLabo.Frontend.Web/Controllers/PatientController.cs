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
}