using System.Text.Json;
using MediLabo.Assessment.API.Models;
using MediLabo.Assessment.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAssessmentService, AssessmentService>();
builder.Services.AddHttpClient("PatientApi",
    client => { client.BaseAddress = new Uri(builder.Configuration["Microservices:PatientApiBaseUrl"]!); });
builder.Services.AddHttpClient("PatientHistoryApi",
    client => { client.BaseAddress = new Uri(builder.Configuration["Microservices:PatientHistoryApiBaseUrl"]!); });
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var assessmentGroup = app.MapGroup("/assessment");

assessmentGroup.MapGet("/{id:int}", async (int id, IAssessmentService assessmentService, IHttpClientFactory factory) =>
{
    var patientClient = factory.CreateClient("PatientApi");
    var patientResponse = await patientClient.GetAsync($"/patients/{id}");
    var patient = await patientResponse.Content.ReadFromJsonAsync<Patient>() ?? throw new JsonException();
    
    var notesClient = factory.CreateClient("PatientHistoryApi");
    var notesResponse = await notesClient.GetAsync($"/notes/patient/{id}");
    var notes = await notesResponse.Content.ReadFromJsonAsync<List<Note>>() ?? [];

    var result = assessmentService.GetResult(patient, notes);
    return Results.Ok(result);
});

app.Run();