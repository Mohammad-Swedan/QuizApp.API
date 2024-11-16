using Microsoft.EntityFrameworkCore;
using QuizForAndroid.BAL.GenericBase;
using QuizForAndroid.BLL.MappingProfiles.eCampus_Prototype.BLL;
using QuizForAndroid.BLL.ServiceInterfaces;
using QuizForAndroid.BLL.Services;
using QuizForAndroid.DAL.Abstracts;
using QuizForAndroid.DAL.Contexts;
using QuizForAndroid.DAL.GenericBase;
using QuizForAndroid.DAL.Repositories;
using QuizForAndroid.DAL.UnitOfWork;

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
//builder.Services.AddScoped<IUserService, UserService>(); // TODO : impliment user services...
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

// Register mapping profile
builder.Services.AddAutoMapper(typeof(MappingProfile));




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
