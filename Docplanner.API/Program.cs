using System.Net.Http.Headers;
using Docplanner.Application;
using Docplanner.Application.Interfaces;
using Docplanner.Application.Models;
using Docplanner.Infrastructure;
using Docplanner.Infrastructure.Helpers;
using Docplanner.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(HttpClients.SlotsService, client =>
{
    var username = builder.Configuration["SlotsService:Username"];
    var password = builder.Configuration["SlotsService:Password"];
    client.BaseAddress = new Uri(builder.Configuration["HttpServices:SlotsService:BaseAddress"]);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
        Convert.ToBase64String($"{username}:{password}".Select(Convert.ToByte).ToArray()));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ISlotsRepository, SlotsRepository>();
builder.Services.AddScoped<ISlotsInput, SlotsRepository>();
builder.Services.AddScoped<ISlotsService, SlotsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/slots", 
        async (string mondayDate, ISlotsService slotsService) => 
            await slotsService.GetAvailableSlots(new GetAvailableSlotsQuery(){MondayDate = mondayDate})
            )
    .WithName("GetSlots")
    .WithOpenApi();

app.Run();