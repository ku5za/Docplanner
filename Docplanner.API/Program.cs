using System.Net.Http.Headers;
using Docplanner.API.Helpers;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/slots", async (IHttpClientFactory factory) =>
    {
        using var client = factory.CreateClient(HttpClients.SlotsService);
        var response = await client.GetAsync($"{SlotsServiceEndpoints.GetWeeklyAvailability}/20240715");
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    })
    .WithName("GetSlots")
    .WithOpenApi();

app.Run();