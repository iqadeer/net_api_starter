using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NetAPI.API.Validations;
using NetAPI.Application.Interfaces;
using NetAPI.Application.Mappings;
using NetAPI.Application.Services;
using NetAPI.Infrastructure;
using NetAPI.Infrastructure.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISpeakerService, SpeakerService>();
builder.Services.AddScoped<ISpeakerRepo, SpeakerEfRepo>();
builder.Services.AddAutoMapper(typeof(SpeakerProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<SpeakerDtoValidator>();
builder.Services.AddSingleton<IPersonRepo, PersonRepo>();
builder.Services.AddSingleton<IPersonService, PersonService>();
builder.Services.AddScoped<IWeatherApiService>(p =>
{
    var weatherList = new List<string>();
    var logger = p.GetRequiredService<ILogger<WeatherApiService>>();
    return new WeatherApiService(logger, weatherList);
});
builder.Services.AddDbContext<SpeakerDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FossDb;Trusted_Connection=True;"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "http://localhost:3000") // Angular dev server
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the policy
app.UseCors("AllowAngularDevClient");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
