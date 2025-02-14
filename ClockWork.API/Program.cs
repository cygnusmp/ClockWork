
using ClockWork.Domain.Interfaces;
using ClockWork.Infrastructure;
using ClockWork.Infrastructure.Repository;

namespace ClockWork.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureDependency(builder.Services);
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app);
        }

        public static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHealthChecks("/hc");

            app.UseHttpsRedirection();
            
            app.UseCors("WorkClockWebApplication");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddCors(options =>
                options.AddPolicy(name: "WorkClockWebApplication",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:7244");
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowCredentials();
                    }
            ));

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ClockWork",
                    Version = "v1",
                    Description = "Application to manage time in job",
                    TermsOfService = new Uri("https://terms.com/"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Badger",
                        Email = string.Empty,
                        Url = new Uri("https://facebook.conm/mariuszpawelec")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Used License",
                        Url = new Uri("https://example.con/license")
                    }
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "ClockWork.API.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddHealthChecks();
        }

        public static void ConfigureDependency(IServiceCollection services)
        {
            services.AddTransient<ITaskRepository, TaskRepository>();
        }
    }
}
