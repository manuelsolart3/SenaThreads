using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application;
using SenaThreads.Infrastructure;



var builder = WebApplication.CreateBuilder(args);


// Configuramos la cadena de conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("ConexionMysql");
if (connectionString != null)
{ //Añadir servicios de las capas
    builder.Services
        .AddApplication()
        .AddInfrastructure(connectionString);
}


// Add services to the container. 
builder.Services.AddControllersWithViews();

//Añadimos servicios de de Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
