using Serilog;
using VerticalSliceArchitecture.API.Configuration;
using VerticalSliceArchitecture.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencies();
builder.Services.AddDatabases();
builder.Services.AddErrorConfig();
builder.Services.AddLoggingConfig();
builder.Services.AddAppSettings();
builder.Services.AddOutputCache();
builder.Services.AddSwagger();
builder.Services.AddHttpClient();
builder.Services.AddValidation();

var app = builder.Build();

app.MapEndpoints();
app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseOutputCache();
app.UseSerilogRequestLogging();

app.UseMiddleware<MaintenanceMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.RunMigrations();
}

app.Run();