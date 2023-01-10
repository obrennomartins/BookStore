using BookStore.Business.Interfaces;
using BookStore.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace BookStore.Business.Services;

public class Service
{
    private readonly INotifier _notifier;

    public Service(INotifier notifier)
    {
        _notifier = notifier;
    }
    
    protected void Notify(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notify(error.ErrorMessage);
        }
    }
    
    protected void Notify(string message)
    {
        _notifier.Handle(new Notification(message));
    }
    
    protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : class
    {
        var validator = validation.Validate(entity);
        if (validator.IsValid) return true;
        
        Notify(validator);
        return false;
    }
}