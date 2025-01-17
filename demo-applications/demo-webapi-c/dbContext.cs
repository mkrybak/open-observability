using Microsoft.EntityFrameworkCore;

namespace demo_webapi_c
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring
            (DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "WeatherForecastDb");
            optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=WeatherForecastDb;Persist Security Info=False;User ID=PaymentUser;Password=PaymentUser;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=600;MultipleActiveResultSets=True");
        }
        public DbSet<WeatherForecast> WeatherForecast { get; set; }
    }
}
