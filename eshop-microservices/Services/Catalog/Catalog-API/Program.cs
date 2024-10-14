var builder = WebApplication.CreateBuilder(args);
//dependency injection
builder.Services.AddCarter();
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly) ;

});

var app = builder.Build();
//Configure HTTPS pipeline

app.MapCarter();

app.Run();
