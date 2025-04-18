
using Containers.Application;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("UniversityDatabase");
builder.Services.AddSingleton<IContainerService, ContainerService>(containerService => new ContainerService(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/containers", (IContainerService containerService) =>
{
    try
    {
        return Results.Ok(containerService.Containers());
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
});

app.Run();

