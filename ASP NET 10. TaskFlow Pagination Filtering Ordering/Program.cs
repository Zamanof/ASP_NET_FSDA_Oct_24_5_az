using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Data;
using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Mapping;
using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Services;
using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Services.Interfaces;
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
        }
        );
    app.MapOpenApi();

}

app.UseAuthorization();

app.MapControllers();

app.Run();
