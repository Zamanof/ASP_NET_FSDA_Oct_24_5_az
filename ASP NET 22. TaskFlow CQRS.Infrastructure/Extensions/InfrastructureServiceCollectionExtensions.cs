using ASP_NET_22._TaskFlow_CQRS.Application.Config;
using ASP_NET_22._TaskFlow_CQRS.Application.Interfaces;
using ASP_NET_22._TaskFlow_CQRS.Application.Storage;
using ASP_NET_22._TaskFlow_CQRS.Infrastructure.Data;
using ASP_NET_22._TaskFlow_CQRS.Infrastructure.Identity;
using ASP_NET_22._TaskFlow_CQRS.Infrastructure.Jwt;
using ASP_NET_22._TaskFlow_CQRS.Infrastructure.Repositories;
using ASP_NET_22._TaskFlow_CQRS.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ASP_NET_22._TaskFlow_CQRS.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection(JwtConfig.SectionName));

        var connectionString = configuration.GetConnectionString("DefaultConnectionString");
        services.AddDbContext<TaskFlowDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITaskAttachmentRepository, TaskAttachmentRepository>();

        services.AddScoped<IFileStorage, LocalDiskStorage>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAuthUserStore, AuthUserStore>();

        return services;
    }
}
