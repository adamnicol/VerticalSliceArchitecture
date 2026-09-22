using Serilog;
using System.Reflection;
using VerticalSliceArchitecture.Infrastructure.Database;
using VerticalSliceArchitecture.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog((services, config) =>
    config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRequestHandlers();
builder.Services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());
builder.Services.AddDependencies();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddOutputCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseOutputCache();
app.UseSerilogRequestLogging();
app.MapEndpoints();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();