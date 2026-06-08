using MediLabo.Gateway.API.Security;
using MediLabo.Gateway.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(options => options.UseInMemoryDatabase("Users"));
builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.AddAuthentication("BasicAuth")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuth", null);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath).AddOcelot(builder.Environment);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

var adminConfig = app.Configuration.GetSection("Admin");
var username = adminConfig["Username"]!;
var password = adminConfig["Password"]!;
var email = adminConfig["Email"]!;

var user = new IdentityUser { UserName = username, Email = email };
await userManager.CreateAsync(user, password);

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();