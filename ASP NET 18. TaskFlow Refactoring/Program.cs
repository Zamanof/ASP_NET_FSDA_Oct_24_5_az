using ASP_NET_18._TaskFlow_Refactoring.Extensions;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger()
                .AddTaskFlowDbContext(builder.Configuration)
                .AddIdentityAndDb(builder.Configuration)
                .AddJwtAuthenticationAndAuthorization(builder.Configuration)
                .AddCorsPolicy()
                .AddFluentValidation()
                .AddAutoMapperAndOtherDI();

var app = builder.Build();

app.UseTaskFlowPipeline();

await app.EnsureRolesSeededAsync();

app.Run();