using EmailService.Extensions;
using EmailService.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register azure service bus
builder.Services.AddSingleton<IAzureServiceBusConsumer , AzureServiceBusConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// add the azure service

app.useAzure();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
