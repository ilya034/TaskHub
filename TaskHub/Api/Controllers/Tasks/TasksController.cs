using Api.Controllers.Tasks.Request;
using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Tasks;

[ApiController]
[Route("tasks")]
public sealed class TasksController : ControllerBase
{
    private readonly ICreateTaskUseCase _createTaskUseCase;
    private readonly IGetTasksUseCase _getTasksUseCase;
    private readonly IGetTaskUseCase _getTaskUseCase;
    private readonly ISetTaskTitleUseCase _setTaskTitleUseCase;
    private readonly IDeleteTaskUseCase _deleteTaskUseCase;
    private readonly IDeleteTasksUseCase _deleteTasksUseCase;

    public TasksController(
        ICreateTaskUseCase createTaskUseCase,
        IGetTasksUseCase getTasksUseCase,
        IGetTaskUseCase getTaskUseCase,
        ISetTaskTitleUseCase setTaskTitleUseCase,
        IDeleteTaskUseCase deleteTaskUseCase,
        IDeleteTasksUseCase deleteTasksUseCase)
    {
        _createTaskUseCase = createTaskUseCase;
        _getTasksUseCase = getTasksUseCase;
        _getTaskUseCase = getTaskUseCase;
        _setTaskTitleUseCase = setTaskTitleUseCase;
        _deleteTaskUseCase = deleteTaskUseCase;
        _deleteTasksUseCase = deleteTasksUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> CreateTaskAsync(
        [FromBody] CreateTaskRequest? request,
        CancellationToken cancellationToken)
    {
        var task = await _createTaskUseCase.CreateTaskAsync(
            request!.Title,
            request.CreatedByUserId,
            cancellationToken);

        return CreatedAtAction(nameof(GetTaskByIdAsync), new { id = task.Id }, task);
    }

    [HttpGet]
    public async Task<ActionResult<TaskListResponse>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var response = await _getTasksUseCase.GetAllTasksAsync(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskResponse>> GetTaskByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var taskResponse = await _getTaskUseCase.GetTaskByIdAsync(id, cancellationToken);

        if (taskResponse is null)
        {
            return NotFound();
        }

        return Ok(taskResponse);
    }

    [HttpPut("{id:guid}/title")]
    public async Task<IActionResult> SetTaskTitleAsync(
        [FromRoute] Guid id,
        [FromBody] SetTaskTitleRequest? request,
        CancellationToken cancellationToken)
    {
        await _setTaskTitleUseCase.SetTaskTitleAsync(id, request!.Title, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTaskByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _deleteTaskUseCase.DeleteTaskByIdAsync(id, cancellationToken);
        if (deleted == false)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _deleteTasksUseCase.DeleteAllTasksAsync(cancellationToken);
        return NoContent();
    }
}
