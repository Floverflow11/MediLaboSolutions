using MediLabo.Gateway.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(options => options.UseInMemoryDatabase("Users"));
builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AuthDbContext>();

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath).AddOcelot();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
var user = new IdentityUser { UserName = "admin", Email = "admin@medilabo.com" };
await userManager.CreateAsync(user, "Admin123!");

await app.UseOcelot();

app.Run();