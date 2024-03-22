using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ClimbingAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Climbs") ?? "Data Source=Climbs.db";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "Climbing API",
         Description = "Handles saved climbing data",
         Version = "v1" });
});
builder.Services.AddSqlite<ClimbsDb>(connectionString);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Climbing API V1");
});

app.MapGet("/", () => "Hello World!");

app.Run();
