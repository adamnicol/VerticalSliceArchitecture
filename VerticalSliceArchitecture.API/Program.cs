using Serilog;
using VerticalSliceArchitecture.API.Configuration;
using VerticalSliceArchitecture.API.Middleware;
using VerticalSliceArchitecture.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencies();
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddRateLimiter(builder.Configuration);
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
app.UseRateLimiter();
app.MapEndpoints();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();