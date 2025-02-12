namespace HealthProyectNET6
{
    public static class HealthCheckExtensions
    {

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddMySql("Server=localhost;Database=ClinicProDB;User=root;Password=tu_password;",
                         name: "MySQL Connection",
                         tags: new[] { "database" })
                .AddMySql("Server=localhost;Database=AnotherDB;User=root;Password=tu_password;",
                         name: "Another MySQL Connection",
                         tags: new[] { "database" })
                .AddUrlGroup(new Uri("https://localhost:44376/api/v1/room/GetRooms"),
                             name: "Room API Endpoint",
                             tags: new[] { "api", "backend" });

            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(10); // Frecuencia de actualización
                options.SetHeaderText("Monitoreo de Servicios"); //Titulo para la interfaz
                // Filtrado por tags
                options.AddHealthCheckEndpoint("Database Health", "/health/db");
                options.AddHealthCheckEndpoint("API Health", "/health/api");
            })
            .AddInMemoryStorage(); // Almacena la información en memoria

            return services;
        }



    }
}
