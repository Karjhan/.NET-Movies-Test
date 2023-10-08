using API.Extensions;
using API.Middlewares;
using Infrastructure.Data;
using Infrastructure.Data.DataContexts;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();
// Configure the HTTP request pipeline.

// Add the ExceptionMiddleware at start of pipeline + StatusCodePagesWithReExecute for 
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

// Configure app to serve static files (default: wwwroot)
app.UseStaticFiles();

app.UseSwaggerDocumentation();

// Apply defined cors policy to exe.
app.UseCors("DefaultCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<MoviesContext>();
var env = services.GetRequiredService<IHostingEnvironment>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await MoviesContextSeed.SeedAsync(context,env);
}
catch (Exception e)
{
    logger.LogError(e, "An error occured during migration!");
}

app.Run();