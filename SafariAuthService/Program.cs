using Ecommerce.Extensions;
using Ecommerce.Services;
using Ecommerce.Services.Iservices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafariAuthService.Data;
using SafariAuthService.Models;
using SafariAuthService.Services;
using SafariAuthService.Services.Iservice;
using SafariAuthService.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Registering the service for connection to the DB

builder.Services.AddDbContext<SafariDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

// Configuring the Identity Framework 

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<SafariDBContext>();


// Registering Automapper services 

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



// Our application services 
builder.Services.AddScoped<IUser, Userservice>();
builder.Services.AddScoped<IJwt, JWTservice>();

// Allow the protection of routes using bearer of token and authorization using policy
builder.AddAuth();
builder.AddminPolicy();
// Configure the JWT options 
builder.Services.Configure<JWToptions>(builder.Configuration.GetSection("JWToptions"));



var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
