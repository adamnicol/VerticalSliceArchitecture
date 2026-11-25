using Serilog;
using VerticalSliceArchitecture.API.Configuration;
using VerticalSliceArchitecture.API.Configuration.Auth;
using VerticalSliceArchitecture.API.Configuration.Caching;
using VerticalSliceArchitecture.API.Configuration.Database;
using VerticalSliceArchitecture.API.Configuration.RateLimiting;
using VerticalSliceArchitecture.API.Configuration.Swagger;

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
services.AddCachePolicies();
services.AddRateLimiters();
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
app.UseSecurityHeaders();
app.UseMaintenanceMode();
app.UseCsrfProtection();
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