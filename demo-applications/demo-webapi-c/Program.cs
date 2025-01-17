using demo_webapi_c;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApiContext>();

        builder.Services
            .AddOpenObservability(x =>
            {
                x.ServiceName = "test-c";
                x.StaticTelemetryService = true;
                x.WithTracing();
                x.WithMetrics();
                x.WithLogs(x =>
                {
                    x.EnvironmentName = true;
                });
            });
        //builder.Services
        //    .AddOpenObservability(x =>
        //    {
        //        //x.StaticTelemetryService = true;
        //    })
        //    .WithTracing(x =>
        //    {
        //        x.EntityFrameworkCoreTracing = true;
        //    })
        //    .WithMetrics(x =>
        //    {
        //        x.RuntimeMetrics = true;
        //    })
        //    .WithLogs(x =>
        //    {
        //        x.EnvironmentName = true;
        //    });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}