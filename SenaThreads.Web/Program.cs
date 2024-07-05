
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
using SenaThreads.Web;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuraciones de entorno
Env.Load();

// Configuración básica de API y controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Configuración de autenticación JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
//Obtener la key de la configuracion y convertila en arreglo de byte
var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

// Configuración de la aplicación y la infraestructura
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Configuración de servicios de Identity
builder.Services.AddIdentityCore<User>(options =>
{
    options.Tokens.PasswordResetTokenProvider = "ShortLivedToken";
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddTokenProvider<ShortLivedTokenProvider<User>>("ShortLivedToken");



// Configuración de autenticación JWT
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

// Añadir políticas CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMultipleOrigins", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "https://test-sena-book.vercel.app",
            "https://test-sena-book-git-stg-senathreads.vercel.app",
            "https://test-sena-book-git-ft-2334-senathreads.vercel.app",
            "https://test-sena-book-git-ft-2335-senathreads.vercel.app",
            "https://test-sena-book.vercel.app/auth/")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
// Aplicar la política CORS por defecto
app.UseCors("AllowMultipleOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
