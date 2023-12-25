using BookingService.Data;
using BookingService.Services;
using BookingService.Services.Iservice;
using CouponService.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITour, ToursService>();
builder.Services.AddScoped<IHotel, HotelsService>();
builder.Services.AddScoped<IBooking, BookingsService>();
builder.Services.AddScoped<ICoupon, CouponsService>();

builder.Services.AddHttpClient("Tours",c=>c.BaseAddress = new Uri(builder.Configuration.GetSection("ServiceUrls:tourservice").Value));
builder.Services.AddHttpClient("Coupons",c=>c.BaseAddress = new Uri(builder.Configuration.GetSection("ServiceUrls:couponservice").Value));
builder.Services.AddHttpClient("Hotels",c=>c.BaseAddress = new Uri(builder.Configuration.GetSection("ServiceUrls:hotelservice").Value));

Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:apikey").Value;
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<SafariDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

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
