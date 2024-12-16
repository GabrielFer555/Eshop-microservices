
var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();


builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

var app = builder.Build();
app.MapCarter();

app.Run();
