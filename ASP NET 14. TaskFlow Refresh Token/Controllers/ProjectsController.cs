using ASP_NET_14._TaskFlow_Refresh_Token.Common;
using ASP_NET_14._TaskFlow_Refresh_Token.DTOs.Project_DTOs;
using ASP_NET_14._TaskFlow_Refresh_Token.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Controllers;

/// <summary>
/// Controller for managing projects.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectsController"/> class.
    /// </summary>
    /// <param name="projectService">Service for project operations.</param>
    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="createProjectDto">The payload used to create the project.</param>
    /// <returns>The created project wrapped in <see cref="ApiResponse{ProjectResponseDto}"/>.</returns>
    /// <response code="201">The project was successfully created.</response>
    /// <response code="400">The request body is invalid.</response>
    [HttpPost]
    //[Authorize(Roles ="Admin, Manager")]
    [Authorize(Policy ="AdminOrManager")]
    public async Task<ActionResult<ApiResponse<ProjectResponseDto>>> Create([FromBody] CreateProjectDto createProjectDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        //throw new KeyNotFoundException();
        var createdProject = await _projectService.CreateAsync(createProjectDto);

        return CreatedAtAction(nameof(GetById), new { id = createdProject.Id },
            ApiResponse<ProjectResponseDto>.SuccessResponse(createdProject, "Project created successfully"));
    }

    /// <summary>
    /// Retrieves a project by its identifier.
    /// </summary>
    /// <param name="id">The project identifier.</param>
    /// <returns>The project details wrapped in <see cref="ApiResponse{ProjectResponseDto}"/>.</returns>
    /// <response code="200">The project was found and returned.</response>
    /// <response code="404">A project with the specified identifier was not found.</response>
    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin, Manager, User")]
    [Authorize(Policy = "UserOrAbove")]
    public async Task<ActionResult<ApiResponse<ProjectResponseDto>>> GetById(int id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project is null)
            return NotFound($"Project with ID {id} not found");

        return Ok(ApiResponse<ProjectResponseDto>.SuccessResponse(project));
    }

    /// <summary>
    /// Retrieves all projects.
    /// </summary>
    /// <returns>A list of all projects wrapped in <see cref="ApiResponse{IEnumerable{ProjectResponseDto}}"/>.</returns>
    /// <response code="200">Returns the list of projects.</response>
    [HttpGet]
    //[Authorize(Roles = "Admin, Manager, User")]
    [Authorize(Policy = "UserOrAbove")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProjectResponseDto>>>> GetAll()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<ProjectResponseDto>>.SuccessResponse(projects));
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="id">The project identifier.</param>
    /// <param name="updateProjectDto">The payload used to update the project.</param>
    /// <returns>The updated project details wrapped in <see cref="ApiResponse{ProjectResponseDto}"/>.</returns>
    /// <response code="200">The project was successfully updated.</response>
    /// <response code="400">The request body is invalid.</response>
    /// <response code="404">A project with the specified identifier was not found.</response>
    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin, Manager")]
    [Authorize(Policy = "AdminOrManager")]
    public async Task<ActionResult<ApiResponse<ProjectResponseDto>>> Update(int id, [FromBody] UpdateProjectDto updateProjectDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedProject = await _projectService.UpdateAsync(id, updateProjectDto);

        if (updatedProject is null)
            return NotFound($"Project with ID {id} not found");

        return Ok(ApiResponse<ProjectResponseDto>.SuccessResponse(updatedProject, "Project updated successfully"));
    }

    /// <summary>
    /// Deletes a project by its identifier.
    /// </summary>
    /// <param name="id">The project identifier.</param>
    /// <returns>Result of the delete operation wrapped in <see cref="ApiResponse{object}"/>.</returns>
    /// <response code="200">The project was successfully deleted.</response>
    /// <response code="404">A project with the specified identifier was not found.</response>
    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var isDeleted = await _projectService.DeleteAsync(id);

        if (!isDeleted)
            return NotFound($"Project with ID {id} not found");

        return Ok(ApiResponse<object>.SuccessResponse(null!, "Project deleted successfully"));
    }
}


// CanCreate
// CanRead
// CanUpdate
// CanDelete
// CanTest
// CanFilan
// CanBesmeken
// CanOther
// CanSome
// CanAny