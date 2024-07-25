using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Configuration;
using System.Text.Json.Serialization;
using VMS;
using VMS.Data;
using VMS.Repository;
using VMS.Repository.IRepository;
using VMS.Services;
using VMS.Services.IServices;
using VMS.AVHubs;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR();

builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
// Add this line  

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddMvc();
builder.Services.AddSwaggerGen(option => {
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
             "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
             "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
             "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
               {
                   {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           },
                           Scheme = "oauth2",
                           Name = "Bearer",
                           In = ParameterLocation.Header,
                       },
                       new List<string>()
                   }
               });

});
builder.Services.AddControllers();
builder.Services.AddDbContext<VisitorManagementDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();
builder.Services.AddScoped<VisitorService>();
builder.Services.AddScoped<IVisitorFormRepository, VisitorFormRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IPurposeOfVisitRepository, PurposeOfVisitRepository>();


builder.Services.AddScoped<IReportRepository, ReportRepository>();


//authentication for backend API
var key = Encoding.ASCII.GetBytes(builder.Configuration["ApiSettings:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireUserName("admin"));
});
 

    builder.Services.AddScoped<IVisitorFormRepository, VisitorFormRepository>();
    builder.Services.AddScoped<IPurposeOfVisitRepository, PurposeOfVisitRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

// Other service registrations

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


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

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
app.MapHub<VisitorHub>("/VisitorHub").RequireCors("CorsPolicy");
app.UseCors("CorsPolicy");
app.Run();

