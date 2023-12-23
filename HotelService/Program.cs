using HotelService.Data;
using HotelService.Extensions;
using HotelService.Services;
using HotelService.Services.Iservice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register services 
builder.Services.AddScoped<ITour, ToursService>();
builder.Services.AddScoped<IHotel, HotelsService>();

builder.Services.AddDbContext<SafariDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});



//Register the Http client service
builder.Services.AddHttpClient("Tours",c=>c.BaseAddress=new Uri(builder.Configuration.GetSection("ServiceUrls:tourservice").Value));


// Add customed services
builder.AddAuth();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMigrations();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
