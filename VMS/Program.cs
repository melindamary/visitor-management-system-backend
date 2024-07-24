using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;
using VMS;
using VMS.Data;
using VMS.Models;
using VMS.Repository.IRepository;
using VMS.Repository;
using VMS.AVHubs;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
// Add this line  

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<VisitorManagementDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.MaxDepth = 32; // Adjust if necessary
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
builder.Services.AddAutoMapper(typeof(MappingConfig));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapHub<VisitorHub>("/ActiveVisitorsignalR").RequireCors("CorsPolicy");
app.UseCors("CorsPolicy");
app.Run();

