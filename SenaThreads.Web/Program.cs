using Microsoft.AspNetCore.Identity;
using SenaThreads.Application;
using SenaThreads.Domain.Users;
using SenaThreads.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

//Añadimos servicios de de Identity
builder.Services.AddIdentityCore<User>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
