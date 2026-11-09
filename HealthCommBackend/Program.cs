using HealthCommBackend.Data;
using HealthCommBackend.Models;
using HealthCommBackend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BackendDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BackendDBContext>()
    .AddSignInManager();

builder.Services.AddScoped<PatientAppointmentService>();
builder.Services.AddScoped<MedicationConditionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//TODO - Following is commented to resolve backend not running
//with the error "Unhandled exception. System.InvalidOperationException: No service
//for type 'Microsoft.AspNetCore.Identity.IEmailSender`1[HealthCommBackend.Models.ApplicationUser]' has been registered."

// app.MapGroup("/identity").MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.Run();
