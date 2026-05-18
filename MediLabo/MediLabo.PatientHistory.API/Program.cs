using MediLabo.PatientHistory.Database;
using MediLabo.PatientHistory.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HistoryDbContext>(options =>
{
    options.UseMongoDB(builder.Configuration.GetConnectionString("MongoDb")!);
});
builder.Services.AddOpenApi();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HistoryDbContext>();
    context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

    if (!context.Notes.Any())
    {
        var seedNotes = new List<Note>
        {
            new()
            {
                PatientId = 1,
                Content = "Le patient déclare qu'il 'se sent très bien' Poids égal ou inférieur au poids recommandé"
            },
            new()
            {
                PatientId = 2,
                Content =
                    "Le patient déclare qu'il ressent beaucoup de stress au travail Il se plaint également que son audition est anormale dernièrement"
            },
            new()
            {
                PatientId = 2,
                Content =
                    "Le patient déclare avoir fait une réaction aux médicaments au cours des 3 derniers mois Il remarque également que son audition continue d'être anormale"
            },
            new() { PatientId = 3, Content = "Le patient déclare qu'il fume depuis peu" },
            new()
            {
                PatientId = 3,
                Content =
                    "Le patient déclare qu'il est fumeur et qu'il a cessé de fumer l'année dernière Il se plaint également de crises d’apnée respiratoire anormales Tests de laboratoire indiquant un taux de cholestérol LDL élevé"
            },
            new()
            {
                PatientId = 4,
                Content =
                    "Le patient déclare qu'il lui est devenu difficile de monter les escaliers Il se plaint également d’être essoufflé Tests de laboratoire indiquant que les anticorps sont élevés Réaction aux médicaments"
            },
            new()
            {
                PatientId = 4, Content = "Le patient déclare qu'il a mal au dos lorsqu'il reste assis pendant longtemps"
            },
            new()
            {
                PatientId = 4,
                Content =
                    "Le patient déclare avoir commencé à fumer depuis peu Hémoglobine A1C supérieure au niveau recommandé"
            },
            new() { PatientId = 4, Content = "Taille, Poids, Cholestérol, Vertige et Réaction" }
        };

        context.Notes.AddRange(seedNotes);
        await context.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var noteGroup = app.MapGroup("/notes");

noteGroup.MapGet("/patient/{id:int}", async (int id, HistoryDbContext context) =>
{
    var notes = await context.Notes
        .Where(note => note.PatientId == id)
        .OrderByDescending(note => note.CreatedAt)
        .ToListAsync();

    return Results.Ok(notes);
});
noteGroup.MapPost("/", async (Note note, HistoryDbContext context) =>
{
    note.CreatedAt = DateTime.UtcNow;

    context.Notes.Add(note);
    await context.SaveChangesAsync();

    return Results.Created($"/notes/{note.Id}", note);
});

app.Run();