using ExamService.Data;
using ExamService.Service.AnswerService;
using ExamService.Service.ExamQuestionService;
using ExamService.Service.ExamService;
using ExamService.Service.Extentions;
using ExamService.Service.QuestionService;
using ExamService.Service.UserServiceClinet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.ComponentModel.Design;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Exam API", Version = "v1" });
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
builder.Services.AddDbContext<ExamsContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("LMS_DB"));
});
var secretBytes = Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"]);
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
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretBytes)
    };
});
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(1);
});
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
builder.Services.AddScoped<IUserServiceClient, UserServiceClient>();
builder.Services.AddScoped<IExamService, ExamServices>();
builder.Services.AddScoped<IQuestionService,QuestionService>();
builder.Services.AddScoped<IAnswerService,AnswerServices>();
builder.Services.AddScoped<IExamQuestionService, ExamQuestionService>();

builder.Services.AddHttpClient<IUserServiceClient, UserServiceClient>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseUserServiceMiddleware();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
