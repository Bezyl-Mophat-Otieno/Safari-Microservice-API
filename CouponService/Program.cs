using CouponService.Data;
using CouponService.Extensions;
using CouponService.Services;
using CouponService.Services.Iservices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Adding custom services 
// This service  will be used to verify the token on protected routes .
builder.AddAuth();
// Configure databases

builder.Services.AddDbContext<SafariDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

// Register Automapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Register services 

builder.Services.AddScoped<Icoupon, CouponsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMigrations();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
