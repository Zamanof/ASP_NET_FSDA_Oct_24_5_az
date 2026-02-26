using FluentValidation.AspNetCore;
using ASP_NET_22._TaskFlow_CQRS.Api.Extensions;
using ASP_NET_22._TaskFlow_CQRS.Application.Extensions;
using ASP_NET_22._TaskFlow_CQRS.Infrastructure.Extensions;

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
