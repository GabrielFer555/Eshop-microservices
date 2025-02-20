using Ordering.Infrastructure;
using Ordering.Application;
using Ordering.API;
using Ordering.Infrastructure.Data.DatabaseExtensions;

var builder = WebApplication.CreateBuilder(args);
//Infrastructure - EF Core
builder.Services.AddApplicationServices(builder.Configuration)
	.AddInfrastructureServices(builder.Configuration)
	.AddApiServices(builder.Configuration);
//Application - MediatR
// API - Carter, HealthChecks

var app = builder.Build();

app.UseApiServices();
await app.UseInfrastructureServices();
if (app.Environment.IsDevelopment())
{
	await app.UseAutoMigration();
}

app.Run();
