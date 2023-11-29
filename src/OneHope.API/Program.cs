using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OneHope.API.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
//show definitions of enums as strings
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add service for managing a sqlserver database that will be managed using ApplicationDBContext
// the connection to the database was defined in appsettings
builder.Services.AddDbContext<ApplicationDBContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1",
    new OpenApiInfo
    {
        Title = "OneHope.API",
        Version = "v1",
        Description = "Esta API provee servicios para alquilar, comprar, devolver o reabastecer portátiles.",
        License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://opensource.org/license/mit/") },
        Contact = new OpenApiContact { Name = "OneHope Team", Email = "daniel.tomas@alu.uclm.es" },
    });
    //this assign operation names, as the actual names they have
    options.CustomOperationIds(apiDescription =>
    {
        return apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
    });
    options.EnableAnnotations(true, true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //this facilitates to generate unique ids for the operations
        c.DisplayOperationId();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
