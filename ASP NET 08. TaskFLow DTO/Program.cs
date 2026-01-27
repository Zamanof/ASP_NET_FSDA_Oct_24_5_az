using ASP_NET_08._TaskFLow_DTO.Data;
using ASP_NET_08._TaskFLow_DTO.Services;
using ASP_NET_08._TaskFLow_DTO.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<TaskFlowDBContext>(
    options => options.UseSqlServer(connectionString)
    );

builder.Services.AddScoped<IProjectService, ProjectSevice>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();

}

app.UseAuthorization();

app.MapControllers();

app.Run();
