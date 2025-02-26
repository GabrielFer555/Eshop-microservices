namespace Auth.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCarter();
            var app = builder.Build();

            app.MapCarter();

            app.Run();
        }
    }
}
