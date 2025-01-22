using Microsoft.AspNetCore.Mvc;
using Refit;
using STD.PL.Consts;
using STD.PL.Interfaces;
using STD.PL.Interfaces.API;
using STD.PL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

//var hackerNewsBaseUrl = Environment.GetEnvironmentVariable(Common.HackerNewsBaseUrl);

var hackerNewsBaseUrl = builder.Configuration[Common.HackerNewsBaseUrl];

builder.Services
    .AddRefitClient<IHackerNewsApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(hackerNewsBaseUrl!));

//builder.Services.AddScoped<IHackerNewsApi>();

builder.Services.AddScoped<IHackerNewsService, HackerNewsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/stories/best", async (IHackerNewsService _hackerNewsService, [FromQuery(Name = "n")] int? n) =>
{
    if (n.GetValueOrDefault() == 0)
        n = 10;

    var result = await _hackerNewsService.GetBestStoriesAsync(n.GetValueOrDefault());

    return TypedResults.Ok(result);
})
.WithName("GetBestStoriesAsync")
.WithOpenApi();

app.Run();
