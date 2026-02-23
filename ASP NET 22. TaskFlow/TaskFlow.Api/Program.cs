using TaskFlow.Api.Extensions;
using TaskFlow.Application.Extensions;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Infrastructure.Extensions;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddIdentityAndJwt(builder.Configuration);

builder.Services.AddSwagger();
builder.Services.AddJwtAuthenticationAndAuthorization(builder.Configuration);
builder.Services.AddCorsPolicy();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseTaskFlowPipeline();
await app.EnsureRolesSeededAsync();

app.Run();
