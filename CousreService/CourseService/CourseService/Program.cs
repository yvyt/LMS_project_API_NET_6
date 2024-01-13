using CourseService.Data;
using CourseService.Service.ClassesService;
using CourseService.Service.CoursesService;
using CourseService.Service.DocumentService;
using CourseService.Service.Extentions;
using CourseService.Service.LessonService;
using CourseService.Service.ResourceService;
using CourseService.Service.TopicService;
using CourseService.Service.UserServiceClinet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Course API", Version = "v1" });
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
builder.Services.AddDbContext<CourseContext>(option =>
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
builder.Services.AddScoped<ICourseService,CourseServices>();
builder.Services.AddScoped<IUserServiceClient, UserServiceClient>();
builder.Services.AddScoped<IClassService, ClassesService>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<ILessonService,LessonService>();
builder.Services.AddScoped<IDocumentService, DocumentServices>();
builder.Services.AddScoped<IResourceService, ResourceService>();

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
