using Ordering.Infrastructure;
using Ordering.Application;
using Ordering.API;


var builder = WebApplication.CreateBuilder(args);
//Infrastructure - EF Core
builder.Services.AddApplicationServices().AddInfrastructureServices(builder.Configuration).AddApiServices();

//Application - MediatR
// API - Carter, HealthChecks

var app = builder.Build();

app.UseApiServices();

app.Run();
