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
//WARNING: Email sending is disabled at this time for demonstration purposes. Please configure email sending for final version.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => { options.SignIn.RequireConfirmedEmail = false; })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BackendDBContext>()
    .AddSignInManager();


builder.Services.AddScoped<IEmailSender<ApplicationUser>, IdentityEmailSender>();

builder.Services.AddScoped<PatientAppointmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapGroup("/identity").MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.Run();
