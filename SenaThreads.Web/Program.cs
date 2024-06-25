
using System.Configuration;
using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SenaThreads.Application;
using SenaThreads.Application.Authentication;
using SenaThreads.Domain.Users;
using SenaThreads.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Configuración de autenticación JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Configuración de servicios de Identity
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();



//Obtener la key de la configuracion y convertila en arreglo de byte
var key = Encoding.ASCII.GetBytes(jwtSettings.Key);


//Servicios de auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => //Configurar la auth de Jwt Bearer
{
    options.SaveToken = true; //guardar el token
    options.TokenValidationParameters = new TokenValidationParameters //parametros de validacion
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        //ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = false,
        //ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});



// Registro del servicio de protecci�n de datos
builder.Services.AddDataProtection();

var app = builder.Build();

// Configurar la política CORS para la URL local
app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:5173")
          .AllowAnyHeader()
          .AllowAnyMethod();
});

// Configurar la política CORS para la URL principal
app.UseCors(policy =>
{
    policy.WithOrigins("https://test-sena-book.vercel.app")
          .AllowAnyHeader()
          .AllowAnyMethod();
});

// Configurar la política CORS para la URL de staging
app.UseCors(policy =>
{
    policy.WithOrigins("https://test-sena-book-git-stg-senathreads.vercel.app")
          .AllowAnyHeader()
          .AllowAnyMethod();
});

// Configurar la política CORS para las ramas de desarrollo
app.UseCors(policy =>
{
    policy.WithOrigins(
        "https://test-sena-book-git-ft-2334-senathreads.vercel.app",
        "https://test-sena-book-git-ft-2335-senathreads.vercel.app")
          .AllowAnyHeader()
          .AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
