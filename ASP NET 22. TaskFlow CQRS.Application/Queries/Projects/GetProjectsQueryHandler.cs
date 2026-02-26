using ASP_NET_22._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_22._TaskFlow_CQRS.Application.Interfaces;
using AutoMapper;
using MediatR;
using System.Data;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Queries.Projects;

internal class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectResponseDto>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectsQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectResponseDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllForUserAsync(request.UserId, request.UserRoles);
        return _mapper.Map<IEnumerable<ProjectResponseDto>>(projects);
    }
}
