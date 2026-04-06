using Api.Filters;
using Api.Controllers.Tasks.Request;
using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Tasks;

[ApiController]
[Route("tasks")]
[ServiceFilter(typeof(RequestLoggingFilter))]
[TypeFilter(typeof(StudentInfoHeadersFilter), Arguments = new object[] { "Kuleshov Ilya", "RI-240948" })]
public sealed class TasksController : ControllerBase
{
    private readonly IManageTaskUseCase _taskUseCase;

    public TasksController(IManageTaskUseCase taskUseCase)
    {
        _taskUseCase = taskUseCase;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidateCreateTaskRequestFilter))]
    public async Task<ActionResult<TaskResponse>> CreateTaskAsync(
        [FromBody] CreateTaskRequest? request,
        CancellationToken cancellationToken)
    {
        var task = await _taskUseCase.CreateTaskAsync(
            request!.Title,
            request.CreatedByUserId,
            cancellationToken);

        return Ok(task);
    }

    [HttpGet]
    public async Task<ActionResult<TaskListResponse>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var response = await _taskUseCase.GetAllTasksAsync(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskResponse>> GetTaskByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var taskResponse = await _taskUseCase.GetTaskByIdAsync(id, cancellationToken);

        if (taskResponse is null)
        {
            return NotFound();
        }

        return Ok(taskResponse);
    }

    [HttpPut("{id:guid}/title")]
    [ServiceFilter(typeof(ValidateSetTaskTitleRequestFilter))]
    public async Task<IActionResult> SetTaskTitleAsync(
        [FromRoute] Guid id,
        [FromBody] SetTaskTitleRequest? request,
        CancellationToken cancellationToken)
    {
        await _taskUseCase.SetTaskTitleAsync(id, request!.Title, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTaskByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _taskUseCase.DeleteTaskByIdAsync(id, cancellationToken);
        if (deleted == false)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskUseCase.DeleteAllTasksAsync(cancellationToken);
        return NoContent();
    }
}
