using FluentValidation;
using Library_.Data;
using Library_.DTO;
using Library_.Interface;
using Library_.Models;
using Library_.Repository;
using Library_.Validation;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using System.Reflection;
using Library_.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();

builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;
                    // Automatic registration of validators in assembly
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });


builder.Services.AddAutoMapper(typeof(AppMappingProfile));


builder.Services.AddDbContext<LibraryContext>(opt =>
    opt.UseInMemoryDatabase("Library"));

Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options=>options.AddPolicy("default", policy=>
policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));

var app = builder.Build();

Seed.SeedData(app);

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    //app.UseSwagger();
    //app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("default");

app.UseAuthorization();

app.UseMiddleware<HttpLoggingMiddleware>();

app.MapControllers();


app.Run();

