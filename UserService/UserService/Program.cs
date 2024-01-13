using MailKit;
using MailService.Models;
using MailService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NETCore.MailKit.Core;
using System.Text;
using UserService.Data;
using UserService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "User API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Need a valid token",
        Name = "Authorization Token",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<IUserService, UserServices>();

builder.Services.AddDbContext<AppicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("LMS_DB"));
});
var secretBytes=Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"]);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthSettings:Audience"],
        ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
        RequireExpirationTime =true,
        IssuerSigningKey=new SymmetricSecurityKey(secretBytes)
    };
});
// add mail config
var emailConfig = builder.Configuration.GetSection("MailSettings")
    .Get<MailSettings>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IMailServices,MailServices>();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan=TimeSpan.FromHours(1);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(op =>
{
    op.Password.RequireDigit = false;
    op.Password.RequireLowercase = false;
    op.Password.RequiredLength = 8;
    op.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    op.User.AllowedUserNameCharacters = string.Empty;


}).AddEntityFrameworkStores<AppicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    List<string> modules = new List<string> { "Course", "Class", "PrivateFile", "Topic", "Lesson", "Resource", "Exam", "Question", "Answer", "ExamQuestion", "User" };
    List<string> action = new List<string> { "Edit", "Delete", "Create", "View" };
    foreach (var module in modules)
    {
        foreach (var ac in action)
        {
            options.AddPolicy($"{ac}{module}", policy => policy.RequireClaim("Permission", $"{ac}:{module}"));

        }
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
