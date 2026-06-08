using MediLabo.Patient.Database;
using MediLabo.Patient.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<PatientDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var patientGroup = app.MapGroup("/patients");

patientGroup.MapGet("/",
    async (PatientDbContext context) => Results.Ok(await context.Patients.AsNoTracking().ToListAsync()));
patientGroup.MapGet("/{id:int}", async (int id, PatientDbContext context) =>
{
    var patient = await context.Patients.FindAsync(id);

    return patient == null ? Results.NotFound() : Results.Ok(patient);
});
patientGroup.MapPost("/", async (Patient patient, PatientDbContext context) =>
{
    context.Patients.Add(patient);
    await context.SaveChangesAsync();

    return Results.Created($"/patients/{patient.Id}", patient);
});
patientGroup.MapPut("/{id:int}", async (int id, Patient patient, PatientDbContext context) =>
{
    if (id != patient.Id)
    {
        return Results.BadRequest("Parameter id and patient id mismatch");
    }

    var existingPatient = await context.Patients.FindAsync(id);

    if (existingPatient == null)
    {
        return Results.NotFound();
    }

    existingPatient.FirstName = patient.FirstName;
    existingPatient.LastName = patient.LastName;
    existingPatient.DateOfBirth = patient.DateOfBirth;
    existingPatient.Gender = patient.Gender;
    existingPatient.Address = patient.Address;
    existingPatient.PhoneNumber = patient.PhoneNumber;

    await context.SaveChangesAsync();

    return Results.NoContent();
});
patientGroup.MapDelete("/{id:int}", async (int id, PatientDbContext context) =>
{
    var patient = await context.Patients.FindAsync(id);

    if (patient == null)
    {
        return Results.NotFound();
    }

    context.Patients.Remove(patient);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PatientDbContext>();
    context.Database.Migrate();
}

app.Run();