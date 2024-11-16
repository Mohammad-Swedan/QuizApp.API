using eCampus_Prototype.BLL.ServiceInterfaces;
using eCampus_Prototype.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.BLL.MappingProfiles.eCampus_Prototype.BLL;
using QuizForAndroid.BLL.ServiceInterfaces;
using QuizForAndroid.BLL.Services;
using QuizForAndroid.DAL.Abstracts;
using QuizForAndroid.DAL.Contexts;
using QuizForAndroid.DAL.GenericBase;
using QuizForAndroid.DAL.Repositories;
using QuizForAndroid.DAL.UnitOfWork;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<QuizAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//TODO:
//Add in defferent way later

// Add repository interfaces and implementations
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<ICollegeRepository, CollegeRepository>();
builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IChoiceRepository, ChoiceRepository>();
builder.Services.AddScoped<IQuizLikesDislikesRepository, QuizLikesDislikesRepository>();
builder.Services.AddScoped<IWriterApplicationRepository, WriterApplicationRepository>();

// Register unit of work ......
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register the generic repository if needed
builder.Services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

// Regoster the generic service
// There is an error (number of generic prams dismatch.
//builder.Services.AddScoped(typeof(IGenericServiceAsync<>), typeof(GenericServiceAsync<,>));

// Register Serivces
builder.Services.AddScoped<IUserService, UserService>(); // TODO : impliment user services...
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddScoped<ICollegeService, CollegeService>();
builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IChoiceService, ChoiceService>();
builder.Services.AddScoped<IQuizLikesDislikesService, QuizLikesDislikesService>();
builder.Services.AddScoped<IWriterApplicationService, WriterApplicationService>();

// Register token service
builder.Services.AddScoped<ITokenService,TokenService>();

// Register mapping profile
builder.Services.AddAutoMapper(typeof(MappingProfile));

var jwtSecret = builder.Configuration["JwtSettings:Secret"];
var jwtIssuer = builder.Configuration["JwtSettings:Issuer"];
var jwtAudience = builder.Configuration["JwtSettings:Audience"];
var jwtExpirationMinutes = builder.Configuration["JwtSettings:ExpirationMinutes"];

if (string.IsNullOrEmpty(jwtSecret))
    throw new Exception("JWT Secret is not configured.");

if (string.IsNullOrEmpty(jwtIssuer))
    throw new Exception("JWT Issuer is not configured.");

if (string.IsNullOrEmpty(jwtAudience))
    throw new Exception("JWT Audience is not configured.");

if (!double.TryParse(jwtExpirationMinutes, out var expirationMinutes))
    throw new Exception("JWT ExpirationMinutes is not configured correctly.");

var key = Encoding.UTF8.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RoleClaimType = ClaimTypes.Role,
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User", "Instructor", "Admin", "SuperAdmin"));
    options.AddPolicy("InstructorPolicy", policy => policy.RequireRole("Instructor", "Admin", "SuperAdmin"));
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin", "SuperAdmin"));
    options.AddPolicy("SuperAdminPolicy", policy => policy.RequireRole("SuperAdmin"));
});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add JWT Authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.  
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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

    // Include XML comments if available
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
