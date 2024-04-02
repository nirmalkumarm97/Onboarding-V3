using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Services;
using EmployeeOnboarding.Repository;
using Microsoft.EntityFrameworkCore;
//using EmployeeOnboarding.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using FluentMigrator.Runner;
using System.Reflection;
using EmployeeOnboarding.Migrations;
using EmployeeOnboarding.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DocumentFormat.OpenXml.Office.CustomUI;
using EmployeeOnboarding.Handler;
using EmployeeOnboarding.Helper;
using Microsoft.OpenApi.Models;
using OpenXmlPowerTools;
using EmployeeOnboarding.BackgroundTask;
//using EmployeeOnboarding.Data.Services;

var builder = WebApplication.CreateBuilder(args);

//Cors Policy
builder.Services.AddCors(options =>
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
                    ));
#region
//JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings:SecretKey").Value;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("JwtSettings:Audience").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings)),
            ClockSkew = TimeSpan.Zero
        };
    });
#endregion
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                 {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                              Type = ReferenceType.SecurityScheme,
                              Id = "Bearer"
                         }
                     },
                     new string[]{}
                 }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultCOnnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddTransient<IEmailSender>(s => new EmailSender("smtp.hostinger.com", 587 , "no-reply@onboarding.ideassion.in" , "N%P-tMmt5'{Wlpu"));
builder.Services.AddTransient<onboardstatusService>();
builder.Services.AddScoped<ILogin, AuthenticateLogin>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IUserDetailsRepository, UserDetailsRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();


//Configuring Fluent Migrator
builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
    .AddFluentMigratorCore()
    .ConfigureRunner(c => c.AddPostgres().WithGlobalConnectionString("DefaultConnection")
    .ScanIn(typeof(AddLogin_202308021630).Assembly).For.Migrations().For.EmbeddedResources());

#region BackGround Task
builder.Services.AddHostedService<StatusUpdateService>();
//builder.Services.AddLogging(loggingBuilder =>
//{
//    loggingBuilder.AddConsole();
//    loggingBuilder.AddFile("logs/ExpiredItemCleanup-.txt");
//});
#endregion BackGround Task
var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");


app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    {
        var db=scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        db.MigrateUp();
    }
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
