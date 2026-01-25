using ASP_NET_09._TaskFlow_Swagger_documentation.Models;
using ASP_NET_09._TaskFlow_Swagger_documentation.Services.Interfaces;
using ASP_NET_09._TaskFlow_Swagger_documentation.DTOs.TaskItem_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_09._TaskFlow_Swagger_documentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskItemsController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemsController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpPost]
    public async Task<ActionResult<TaskItemResponseDto>> Create([FromBody] CreateTaskItemDto createTaskItem)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var createdTaskItem = await _taskItemService.CreateAsync(createTaskItem);

            return CreatedAtAction(
                                    nameof(GetById), 
                                    new { id = createdTaskItem.Id }, 
                                    createdTaskItem);

        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    // http://localhost:5012/api/Projects
    public async Task<ActionResult<IEnumerable<TaskItemResponseDto>>> GetAll()
    {
        var taskItems = await _taskItemService.GetAllAsync();
        return Ok(taskItems);
    }

    [HttpGet("{id}")]
    // http://localhost:5012/api/Projects/1
    public async Task<ActionResult<TaskItemResponseDto>> GetById(int id)
    {
        var taskItem = await _taskItemService.GetByIdAsync(id);
        if (taskItem is null)
            return NotFound($"TaskItem with ID {id} not found");

        return Ok(taskItem);
    }
    [HttpGet("project/{projectId}")]
    // http://localhost:5012/api/Projects/project/1
    public async Task<ActionResult<IEnumerable<TaskItemResponseDto>>> GetByProjectId(int projectId)
    {
        var taskItems = await _taskItemService.GetByProjectIdAsync(projectId);
        return Ok(taskItems);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<TaskItemResponseDto>> Update(int id, [FromBody] UpdateTaskItemDto updateTaskItem)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedTaskItem = await _taskItemService.UpdateAsync(id, updateTaskItem);

        if (updatedTaskItem is null)
            return NotFound($"TaskItem with ID {id} not found");

        return Ok(updatedTaskItem);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var IsDeleted = await _taskItemService.DeleteAsync(id);

        if (!IsDeleted)
            return NotFound($"TaskItem with ID {id} not found");

        return NoContent();
    }
}
