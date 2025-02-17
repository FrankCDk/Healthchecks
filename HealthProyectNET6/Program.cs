using HealthChecks.UI.Client;
using HealthProyectNET6;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCustomHealthChecks();
//builder.Services.AddHealthChecks()
//    .AddMySql("Server=localhost;Database=ClinicProDB;User=root;Password=Dike9930;",
//                  name: "Conexi�n a base de datos.")
//    .AddUrlGroup(new Uri("https://localhost:44376/api/v1/room/GetRooms"), name: "Servicio Backend");


//builder.Services.AddHealthChecksUI(options =>
//{
//    options.SetEvaluationTimeInSeconds(10);
//    options.AddHealthCheckEndpoint("API Health Check", "/health");
//}).AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapHealthChecks("/health", new HealthCheckOptions
//{
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,// Respuesta en formato JSON para el UI
//});

//Health encargado de las conexiones a las base de datos
app.MapHealthChecks("/health/db", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = (check) => check.Tags.Contains("database")
});

//Health encargado de las conexiones a las API
app.MapHealthChecks("/health/api", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = (check) => check.Tags.Contains("api")
});

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/health") ||
        context.Request.Path.StartsWithSegments("/health-ui"))
    {
        var apiKey = context.Request.Query["apikey"];
        if (apiKey != "MiClaveSegura")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
    }
    await next();
});


app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui"; // Ruta del dashboard visual
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
