using ASP_NET_22._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_22._TaskFlow_CQRS.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Commands.Projects;

internal class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectResponseDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ProjectResponseDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdWithTasksAsync(request.Id);
        if (project is null) return null;
        _mapper.Map(request.UpdateProjectDto, project);
        await _projectRepository.UpdateAsync(project);
        return _mapper.Map<ProjectResponseDto>(project);
    }
}
