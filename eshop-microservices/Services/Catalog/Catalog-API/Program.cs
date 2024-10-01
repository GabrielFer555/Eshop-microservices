var builder = WebApplication.CreateBuilder(args);
//dependency injection

var app = builder.Build();

//Configure HTTPS pipeline


app.Run();
