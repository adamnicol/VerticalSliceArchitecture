using Serilog;
using VerticalSliceArchitecture.API.Configuration;
using VerticalSliceArchitecture.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencies();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddRateLimiter(builder.Configuration);
builder.Services.AddAppSettings();
builder.Services.AddOutputCache();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.RunMigrations();
}

app.MapEndpoints();
app.UseHttpsRedirection();
app.UseOutputCache();
app.UseSerilogRequestLogging();
app.UseRateLimiter();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();