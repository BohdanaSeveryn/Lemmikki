using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;
using Lemmikki;
using Lemmikki.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<VeterinaryDatabase>();
builder.Services.AddScoped<UI>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Lemmikki";
    config.Title = "Lemmikki v1";
    config.Version = "v1";
});

var app = builder.Build();
app.UseOpenApi();
app.UseSwaggerUi(config =>
{
    config.DocumentTitle = "Lemmikki";
    config.Path = "/swagger";
    config.DocumentPath = "/swagger/Lemmikki/swagger.json";
    config.DocExpansion = "list";
});

app.MapControllers();

// Launch the console interface (optional)
/*using (var scope = app.Services.CreateScope())
{
    var ui = scope.ServiceProvider.GetRequiredService<UI>();
    ui.Run(); 
}*/
app.MapGet("/", () => "Tervetuloa Lemmikki API:iin!");

app.Run();
