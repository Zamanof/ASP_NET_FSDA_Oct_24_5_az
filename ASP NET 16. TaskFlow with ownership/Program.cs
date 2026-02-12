using ASP_NET_16._TaskFlow_with_ownership.Authorization;
using ASP_NET_16._TaskFlow_with_ownership.Data;
using ASP_NET_16._TaskFlow_with_ownership.Mapping;
using ASP_NET_16._TaskFlow_with_ownership.Middlewares;
using ASP_NET_16._TaskFlow_with_ownership.Models;
using ASP_NET_16._TaskFlow_with_ownership.Services;
using ASP_NET_16._TaskFlow_with_ownership.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(
        options =>
        {
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

            // JWT options for Swaggeer
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = """
                JWT Suthorization header using the Bearer scheme. 
                Example: Authorization: Bearer {token}
                """,
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
        });


var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<TaskFlowDBContext>(
    options => options.UseSqlServer(connectionString)
    );

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;

        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false;
    }

)
    .AddEntityFrameworkStores<TaskFlowDBContext>()
    .AddDefaultTokenProviders();

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    )
    .AddJwtBearer(
        options=>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience =true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                ClockSkew = TimeSpan.Zero
            };
        }
    );

// Authorization policies
builder.Services.AddAuthorization(
    options=>
    {
        options
        .AddPolicy(
            "AdminOnly", 
            policy => 
                policy.RequireRole("Admin"));
        options
        .AddPolicy(
            "AdminOrManager", 
            policy => 
                policy.RequireRole("Admin", "Manager"));
        options
        .AddPolicy(
            "UserOrAbove", 
            policy => 
                policy.RequireRole("Admin", "Manager", "User"));
        options
         .AddPolicy(
            "ProjectOwnerOrAdmin", 
            policy => 
                policy.Requirements.Add(new ProjectOwnerOrAdminRequirment()));
        options
         .AddPolicy(
            "ProjectMemberOrHigher",
            policy =>
                policy.Requirements.Add(new ProjectMemberOrHigherRequirment()));
        options
         .AddPolicy(
            "TaskStatusChange",
            policy =>
                policy.Requirements.Add(new TaskStatusChangeRequirment()));
    }
    );

builder.Services.AddScoped<IAuthorizationHandler, ProjectOwnerOrAdminHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ProjectMemberOrHigherHandler>();
builder.Services.AddScoped<IAuthorizationHandler, TaskStatusChangeHandler>();

builder.Services.AddCors(
    options =>
    {
        options.AddDefaultPolicy(
            policy =>
            {
                policy.WithOrigins(
                    "http://localhost:3000",
                    "http://127.0.0.1:3000"
                    )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            }
            );
    }
    );

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Services
builder.Services.AddScoped<IProjectService, ProjectSevice>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<IAuthService, AuthService>();

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

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        await RoleSeeder.SeedRolesAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured while seeding roles");
    }
}

    app.Run();
