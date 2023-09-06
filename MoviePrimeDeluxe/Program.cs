using Microsoft.EntityFrameworkCore;
using MoviePrimeDeluxe.Business.Abstract;
using MoviePrimeDeluxe.Business.Concrete;
using MoviePrimeDeluxe.DataAccess;
using MoviePrimeDeluxe.DataAccess.Abstract;
using MoviePrimeDeluxe.DataAccess.Concrete;
using System.Text.Json.Serialization;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviePrimeDeluxe.Entities;
using FluentValidation.AspNetCore;
using FluentValidation;
using MoviePrimeDeluxe.Validation;
using Microsoft.AspNetCore.Mvc;
using MoviePrimeDeluxe.Entities.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<MoviePrimeDeluxeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    options =>
    {
        options.CommandTimeout(60);
        options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    }), ServiceLifetime.Singleton
    );

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMovieService, MovieManager>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
//builder.Services.AddScoped<IValidator<Movie>, MovieValidator>();
//builder.Services.AddScoped<IValidator<UserRegister>, UserValidator>();
//builder.Services.AddScoped<IValidator<WatchedMovie>, WatchedMovieValidator>();

builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
   options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
        options.JsonSerializerOptions.MaxDepth = 3;
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:SecretKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<MoviePrimeDeluxeContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MoviePrimeDeluxe_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(builder =>
    //{
    //    builder.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieApplicationWebApi v1");
    //    builder.RoutePrefix = string.Empty;
    //});
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
