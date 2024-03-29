using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using OneHope.Web.Areas.Identity;
using OneHope.Web.Data;
using PortatilesAPI;
using OneHope.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();

//adding the service created for connecting to the OneHope.API
builder.Services.AddHttpClient<swaggerClient>();

//adding an In-memory state container services
//https://learn.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-6.0&pivots=server#in-memory-state-container-service-server
builder.Services.AddScoped<ReabastecerPortatilStateContainer>();

//adding an In-memory state container services
//https://learn.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-6.0&pivots=server#in-memory-state-container-service-server
builder.Services.AddScoped<ComprarPortatilStateContainer>();

//the environment variable is defined in Properties\launchsettings.json
builder.Services.AddScoped<swaggerClient>(sp =>
        new swaggerClient(Environment.GetEnvironmentVariable("swaggerClient_API"), new HttpClient())
    );
builder.Services.AddScoped<DevolverPortatilesStateContainer>();

//Servicio para mantener el estado de la p�gina web
builder.Services.AddScoped<AlquilarPortatilStateContainer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
