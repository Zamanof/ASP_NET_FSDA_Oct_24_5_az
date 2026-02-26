using ASP_NET_22._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_22._TaskFlow_CQRS.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Queries.Projects;

internal class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectResponseDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }
    public async Task<ProjectResponseDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdWithTasksAsync(request.Id);
        return project is null ? null! : _mapper.Map<ProjectResponseDto>(project);
    }
}
