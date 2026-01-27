using ASP_NET_08._TaskFLow_DTO.Models;
using ASP_NET_08._TaskFLow_DTO.Services.Interfaces;
using ASP_NET_08._TaskFLow_DTO.DTOs.Project_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_08._TaskFLow_DTO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> Create([FromBody]CreateProjectDto createProjectDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdProject = await _projectService.CreateAsync(createProjectDto);

        return CreatedAtAction(nameof(GetById), new { id = createdProject.Id }, createdProject);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponseDto>> GetById(int id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project is null) return NotFound($"Project with ID {id} not found");
        return Ok(project);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectResponseDto>>> GetAll()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(projects);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectResponseDto>> Update(int id,[FromBody] UpdateProjectDto updateProjectDto)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var updatedProject = await _projectService.UpdateAsync(id, updateProjectDto);

        if (updatedProject is null) 
            return NotFound($"Project with ID {id} not found");

        return Ok(updatedProject);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var IsDeleted = await _projectService.DeleteAsync(id);

        if(!IsDeleted) 
            return NotFound($"Project with ID {id} not found");

        return NoContent();
    }

}
