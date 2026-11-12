using Serilog;
using VerticalSliceArchitecture.API.Configuration;
using VerticalSliceArchitecture.API.Configuration.Auth;
using VerticalSliceArchitecture.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpoints();
services.AddEndpointsApiExplorer();
services.AddDependencies();
services.AddDatabases();
services.AddAuthentication(builder.Configuration);
services.AddAuthorization();
services.AddErrorConfiguration();
services.AddLoggingConfiguration();
services.AddValidation();
services.AddHealthCheckConfiguration();
services.AddSettings();
services.AddOutputCache();
services.AddSwagger(builder.Configuration);
services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseSerilogRequestLogging();

app.UseMiddleware<HeadersMiddleware>();
app.UseMiddleware<MaintenanceMiddleware>();

app.MapHealthChecks();
app.UseAuthentication();
app.UseAuthorization();
app.UseOutputCache();
app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.RunMigrations();
}

app.Run();