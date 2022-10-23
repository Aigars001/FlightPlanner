using AutoMapper;
using flightPlanner;
using flightPlanner.Filters;
using FlightPlannerCore.Models;
using FlightPlannerCore.Services;
using FlightPlannerCore.Validations;
using FlightPlannerCore.Validations.SearchFlightRequestValidations;
using flightPlannerData;
using FlightPlannerData;
using FlightPlannerServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthorizationHandler>("BasicAuthentication", null);

builder.Services.AddDbContext<FlightPlannerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Flight-Planner"));
});

builder.Services.AddScoped<IFlightPlannerDbContext, FlightPlannerDbContext>();

builder.Services.AddScoped<IDbService, DbService>();

builder.Services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
builder.Services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
builder.Services.AddScoped<IEntityService<User>, EntityService<User>>();

builder.Services.AddScoped<IFlightService, FlightService>();

builder.Services.AddScoped<IFlightValidator, CarrierValidator>();
builder.Services.AddScoped<IFlightValidator, FlightTimeValidator>();
builder.Services.AddScoped<IFlightValidator, FlightAirportValidator>();

builder.Services.AddScoped<IAirportValidator, AirportCodeValidator>();
builder.Services.AddScoped<IAirportValidator, AirportCountryValidator>();
builder.Services.AddScoped<IAirportValidator, AirportCityValidator>();

builder.Services.AddSingleton<IMapper>(AutoMapperConfig.CreateMapper());

builder.Services.AddScoped<ISearchFlightRequestValidator, AirportValueValidator>();
builder.Services.AddScoped<ISearchFlightRequestValidator, SameAirportValidator>();
builder.Services.AddScoped<ISearchFlightRequestValidator, DepartureDateValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
