using Serilog;
using VerticalSliceArchitecture.API.Configuration;
using VerticalSliceArchitecture.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencies();
builder.Services.AddDatabases();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddErrorConfiguration();
builder.Services.AddLoggingConfiguration();
builder.Services.AddValidation();
builder.Services.AddHealthCheckConfiguration();
builder.Services.AddSettings();
builder.Services.AddOutputCache();
builder.Services.AddSwagger();

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