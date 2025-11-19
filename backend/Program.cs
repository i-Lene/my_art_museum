using backend.Interfaces;
using backend.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];

builder.Services.AddHttpClient("MuseumApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "MyMuseumApp/1.0");
});

builder.Services.AddScoped<IMuseumApiService, MuseumApiService>();
builder.Services.AddScoped<IArtworkService, ArtworkService>();

var app = builder.Build();

app.UseCors("AllowReactApp");

app.MapControllers();
app.Run();
