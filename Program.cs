using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ClimbingAPI.Models;
using Microsoft.AspNetCore.Http.Features;

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

//returns all climbs
app.MapGet("/climbs", async (ClimbsDb db) => await db.Climbs.ToListAsync());

app.MapGet("/climbs/{date}", async (ClimbsDb db, string date) => 
{
   return await db.Climbs
      .Where(b => b.Date.Equals(date))
      .ToListAsync();
});

app.MapPost("/climb", async (ClimbsDb db, Climb climb) =>
{
    await db.Climbs.AddAsync(climb);
    await db.SaveChangesAsync();
    return Results.Created($"/climb/{climb.Id}", climb);
});

app.MapDelete("/climb/{id}", async (ClimbsDb db, int id) =>
{
   var climb = await db.Climbs.FindAsync(id);
   if (climb is null)
   {
      return Results.NotFound();
   }
   db.Climbs.Remove(climb);
   await db.SaveChangesAsync();
   return Results.Ok();
});

app.Run();
