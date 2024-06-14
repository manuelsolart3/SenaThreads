
using Microsoft.AspNetCore.Identity;
using SenaThreads.Application;
using SenaThreads.Domain.Users;
using SenaThreads.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();


builder.Services.AddApplication();

//Añadimos servicios de de Identity
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Registro del servicio de protección de datos
builder.Services.AddDataProtection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

//confi para el appsettings
//builder.Configuration
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
//    .AddEnvironmentVariables();

//Instalamos paquete entityframworkcore inmemory sirve para utilizar una Bd en memoria temporal
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseInMemoryDatabase("InMemoryDatabase"));
