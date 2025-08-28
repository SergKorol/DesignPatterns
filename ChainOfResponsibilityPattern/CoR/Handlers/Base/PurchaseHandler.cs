using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers.Base;

public abstract class PurchaseHandler
{
    private PurchaseHandler? _nextHandler;

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