using System.Net.Http.Headers;
using System.Text;
using MediLabo.Frontend.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IPatientService, PatientService>(client =>
{
    var gatewayConfig = builder.Configuration.GetSection("Gateway");
    client.BaseAddress = new Uri(gatewayConfig["BaseUrl"]!);
    var username = gatewayConfig["username"]!;
    var password = gatewayConfig["password"]!;
    var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
});

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