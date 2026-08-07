using FleetErp.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace FleetErp.Api.Common;

public static class ResultExtensions
{
    /// <summary>
    /// Convert a Result to an IActionResult with appropriate HTTP status code.
    /// </summary>
    public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller)
    {
        return result switch
        {
            { IsSuccess: true } => controller.Ok(result.Value),
            { ErrorType: ResultErrorType.NotFound } => controller.NotFound(new { error = result.Error }),
            { ErrorType: ResultErrorType.Conflict } => controller.Conflict(new { error = result.Error }),
            _ => controller.BadRequest(new { error = result.Error })
        };
    }

    /// <summary>
    /// Convert a Result (no value) to an IActionResult.
    /// </summary>
    public static IActionResult ToActionResult(this Result result, ControllerBase controller)
    {
        return result switch
        {
            { IsSuccess: true } => controller.NoContent(),
            { ErrorType: ResultErrorType.NotFound } => controller.NotFound(new { error = result.Error }),
            { ErrorType: ResultErrorType.Conflict } => controller.Conflict(new { error = result.Error }),
            _ => controller.BadRequest(new { error = result.Error })
        };
    }

    /// <summary>
    /// Convert a Result to a Created response.
    /// </summary>
    public static IActionResult ToCreatedResult<T>(
        this Result<T> result,
        ControllerBase controller,
        string actionName,
        object? routeValues = null)
    {
        return result switch
        {
            { IsSuccess: true } => controller.CreatedAtAction(actionName, routeValues, result.Value),
            { ErrorType: ResultErrorType.NotFound } => controller.NotFound(new { error = result.Error }),
            { ErrorType: ResultErrorType.Conflict } => controller.Conflict(new { error = result.Error }),
            _ => controller.BadRequest(new { error = result.Error })
        };
    }
}
