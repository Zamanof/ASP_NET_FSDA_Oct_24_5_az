using ASP_NET_11._Validation.Data;
using ASP_NET_11._Validation.DTOs.Project_DTOs;
using ASP_NET_11._Validation.Mapping;
using ASP_NET_11._Validation.Middlewares;
using ASP_NET_11._Validation.Services;
using ASP_NET_11._Validation.Services.Interfaces;
using ASP_NET_11._Validation.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(
        options => {
            options.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Version = "v1",
                Title = "TaskFlow API",
                Description = "This API includes full CRUD operations for the TaskFlow project.",
                Contact = new OpenApiContact
                {
                    Name = "TaskFlow Team",
                    Email = "support@taskflow.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/license/mit")
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        }

    );


var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<TaskFlowDBContext>(
    options => options.UseSqlServer(connectionString)
    );

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IProjectService, ProjectSevice>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

//builder.Services.AddScoped<IValidator<CreateProjectDto>, CreateProjectValidator>();
//builder.Services.AddScoped<IValidator<UpdateProjectDto>, UpdateProjectValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskFlow API v1");
            options.RoutePrefix = string.Empty;
            options.EnableFilter();
            options.EnableTryItOutByDefault();
            options.DisplayRequestDuration();
        }
        );
    app.MapOpenApi();

}
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
