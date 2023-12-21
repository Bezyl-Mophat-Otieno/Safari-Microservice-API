using CouponService.Extensions;
using Microsoft.EntityFrameworkCore;
using ToursService.Data;
using ToursService.Services;
using ToursService.Services.Iservice;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database connection 

builder.Services.AddDbContext<SafariDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

// custom services extended inside the extensions
builder.AddAuth();

// Register Automapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Register Services for DI.
builder.Services.AddScoped<ITour, ToursafariService>();
builder.Services.AddScoped<Iimage, ImagetourService>();



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
