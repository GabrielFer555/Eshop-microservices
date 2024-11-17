



using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddCarter();
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!); //adding health checks
builder.Services.AddValidatorsFromAssembly(assembly) ;
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database") ?? throw new Exception("Failed to connect"));
}).UseLightweightSessions();

//configuring initial data for development enviroment 
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly) ;
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));

});

var app = builder.Build();
//Configure HTTPS pipeline
app.UseHealthChecks("/health", 
    new HealthCheckOptions()
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }); 

app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
