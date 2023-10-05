using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Configure app to serve static files (default: wwwroot)
app.UseStaticFiles();

app.UseSwaggerDocumentation();

// Apply defined cors policy to exe.
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();