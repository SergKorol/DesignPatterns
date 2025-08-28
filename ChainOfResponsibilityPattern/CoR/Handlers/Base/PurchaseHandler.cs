using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Logger;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers.Base;

public abstract class PurchaseHandler(ILogger logger)
{
    private PurchaseHandler? _nextHandler;
    protected readonly ILogger Logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public PurchaseHandler SetNext(PurchaseHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public PurchaseContext Handle(PurchaseContext context)
    {
        if (context.IsProcessed)
            return context;

        try
        {
            ProcessRequest(context);
            
            if (!context.IsProcessed && _nextHandler != null)
            {
                return _nextHandler.Handle(context);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error in {GetType().Name}: {ex.Message}");
            context.Result = PurchaseResult.InvalidInput;
            context.ErrorMessage = ex.Message;
            context.IsProcessed = true;
            
            Rollback(context);
        }

        return context;
    }

    protected abstract void ProcessRequest(PurchaseContext context);
    
    protected virtual void Rollback(PurchaseContext context) { }
}