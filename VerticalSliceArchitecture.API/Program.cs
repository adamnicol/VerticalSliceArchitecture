using Serilog;
using VerticalSliceArchitecture.API.Configuration;
using VerticalSliceArchitecture.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencies();
builder.Services.AddDatabases();
builder.Services.AddErrorConfiguration();
builder.Services.AddLoggingConfiguration();
builder.Services.AddAppSettings();
builder.Services.AddOutputCache();
builder.Services.AddSwagger();
builder.Services.AddValidation();
builder.Services.AddHealthCheckConfiguration();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseSerilogRequestLogging();

app.UseMiddleware<HeadersMiddleware>();
app.UseMiddleware<MaintenanceMiddleware>();

app.MapHealthChecks();
app.UseOutputCache();
app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.RunMigrations();
}

app.Run();