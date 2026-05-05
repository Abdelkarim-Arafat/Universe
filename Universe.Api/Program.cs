using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scalar.AspNetCore;
using Serilog;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Universe.Api.ExceptionHandler;
using Universe.Core.Entities;
using Universe.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", policy =>
        policy.SetIsOriginAllowed(origin =>
            origin == "http://localhost:3000" ||
            origin == "https://playful-torrone-6e1691.netlify.app" 
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
    );
});

builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("ReadLimiter", httpContext =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 120,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0
        })
    );

    options.AddPolicy("WriteLimiter", httpContext =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 20,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0
        })
    );

    options.AddPolicy("AuthStrict", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter (
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0,
            })
    );

    options.AddPolicy("SignupLimiter", httpContext =>

        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress?.ToString(),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 3,
                Window = TimeSpan.FromMinutes(5),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0,
            })
    );

    options.AddPolicy("EmailLimiter", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress?.ToString(),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 2,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0,
            })
    );

    options.AddConcurrencyLimiter("FixedLimiter", options =>
    {
        options.PermitLimit = 1000;
        options.QueueLimit = 100;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});


builder.Services.AddInfrastructureDependences(builder.Configuration);
builder.Services.AddApplicationsDependences(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Host.UseSerilog((context, configuration) =>
    configuration
    .MinimumLevel.Information()
    .WriteTo.Console()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//app.UseStaticFiles();


app.UseCors("AllowedOrigins");

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    Authorization =
    [
        new HangfireCustomBasicAuthenticationFilter
        {
            User = app.Configuration.GetValue<string>("HangfireSettings:Username"),
            Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
        }
    ],
    DashboardTitle = "Universe Dashboard",
    //IsReadOnlyFunc = (DashboardContext context) => true
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();