namespace HealthProyectNET6
{
    public static class HealthCheckExtensions
    {

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {

            services.AddHealthChecks()
                .AddMySql()
                .AddUrlGroup(new Uri("https://localhost:44376/api/v1/room/GetRooms"), name: "Backend de prueba");

            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(10); // Frecuencia de actualización
                options.AddHealthCheckEndpoint("API Health Check", "/health");
            }).AddInMemoryStorage(); //Almacena la información en memoria


            return services;
        }



    }
}
