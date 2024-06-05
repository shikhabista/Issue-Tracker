using Microsoft.AspNetCore.Mvc;

namespace IT_Web.Extensions;

public static class ControllerExtensions
{
    public static IActionResult SendSuccess(this Controller controller, string msg, object? data = null)
    {
        return controller.Ok(new { Message = msg, Content = data });
    }

    public static IActionResult SendError(this Controller controller, string msg)
    {
        return controller.BadRequest(new {Error = new {Message = msg}});
    }
}