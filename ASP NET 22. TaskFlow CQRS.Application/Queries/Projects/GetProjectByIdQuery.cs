using ASP_NET_22._TaskFlow_CQRS.Application.DTOs;
using MediatR;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Queries.Projects;

public record GetProjectByIdQuery(int Id) : IRequest<ProjectResponseDto>;
