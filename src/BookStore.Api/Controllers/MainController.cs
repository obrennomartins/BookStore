using BookStore.Business.Interfaces;
using BookStore.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStore.Api.Controllers;

[ApiController]
public class MainController : ControllerBase
{
    private readonly INotifier _notifier;

    public MainController(INotifier notifier)
    {
        _notifier = notifier;
    }
    
    protected bool IsValidOperation()
    {
        return !_notifier.HasNotification();
    }
    
    protected ActionResult CustomResponse(object? result = null)
    {
        if (IsValidOperation())
        {
            return Ok(result);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            {"Messages", _notifier.GetNotifications().Select(n => n.Message).ToArray()}
        }));
    }
    
    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyInvalidModelError(modelState);
        return CustomResponse();
    }
    
    protected void NotifyInvalidModelError(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            NotifyError(errorMessage);
        }
    }
    
    protected void NotifyError(string message)
    {
        _notifier.Handle(new Notification(message));
    }
}