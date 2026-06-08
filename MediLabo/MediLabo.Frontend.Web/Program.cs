using MediLabo.Frontend.Web.Extensions;
using MediLabo.Frontend.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddGatewayHttpClient<IPatientService, PatientService>(builder.Configuration);
builder.Services.AddGatewayHttpClient<INoteService, NoteService>(builder.Configuration);
builder.Services.AddGatewayHttpClient<IAssessmentService, AssessmentService>(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Patient}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();