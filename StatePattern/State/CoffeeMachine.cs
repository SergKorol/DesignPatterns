using StatePattern.State.States;

namespace StatePattern.State;

public class CoffeeMachine
{
    private ICoffeeMachineState _state;

    public int BeverageCount { get; set; }
    public decimal CurrentBalance { get; internal set; }

    public CoffeeMachine(int beverageCount)
    {
        if (beverageCount <= 0)
        {
            _state = new OutOfStockState(this);
        }
        else
        {
            _state = new NoMoneyState(this);
        }

        BeverageCount = beverageCount;
        CurrentBalance = 0;
    }

    public void SetState(ICoffeeMachineState state)
    {
        _state = state;
    }

    public void InsertMoney(decimal amount)
    {
        _state.InsertMoney(amount);
    }

    public void SelectBeverage()
    {
        _state.SelectBeverage();
    }

    public void DispenseBeverage()
    {
        _state.DispenseBeverage();
    }

    public void CancelTransaction()
    {
        _state.CancelTransaction();
    }

    public void AddMoney(decimal amount)
    {
        CurrentBalance += amount;
        Console.WriteLine($"Money added: ${amount}. Current balance: ${CurrentBalance}");
    }

    public void ReturnMoney()
    {
        Console.WriteLine($"Returning ${CurrentBalance} to customer");
        CurrentBalance = 0;
    }

    public void DecrementBeverageCount()
    {
        if (BeverageCount > 0)
        {
            BeverageCount--;
        }
    }

    public string GetCurrentState()
    {
        return _state.GetType().Name;
    }
}