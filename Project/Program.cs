using Project.Models;
using Project.Servcies;
using Microsoft.OpenApi.Models;
using Project.Persistance.SQLServer.Repasitories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// ✅ Swagger konfiguratsiyasi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Client API",
        Version = "v1",
        Description = "API for managing and processing clients"
    });
});

builder.Services.AddSingleton(typeof(QueueService<ClientData>));
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddHostedService<QueryService>();
builder.Services.AddHostedService<ProcessService>();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // ✅ Swagger UI ni yoqish
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Client API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

