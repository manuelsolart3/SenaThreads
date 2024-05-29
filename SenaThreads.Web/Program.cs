using MediatR;
using FluentValidation;
using System.Reflection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SenaThreads.Domain.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// Configuramos la cadena de conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("ConexionMysql");

//Configurar MediatR
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
//builder.Services.AddIdentityCore<User>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddApiEndpoints();

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(configuration.));

//Configurar FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();  // HabilitaR la validación automática de modelos


// Add services to the container.
builder.Services.AddControllersWithViews();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
