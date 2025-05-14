namespace StatePattern.State.States;

public class NoMoneyState(CoffeeMachine coffeeMachine) : ICoffeeMachineState
{
    public void InsertMoney(decimal amount)
    {
        if (amount > 0)
        {
            coffeeMachine.AddMoney(amount);
            coffeeMachine.SetState(new HasMoneyState(coffeeMachine));
        }
        else
        {
            Console.WriteLine("Please insert a valid amount of money");
        }
    }

    public void SelectBeverage()
    {
        Console.WriteLine("Please insert money first");
    }

    public void DispenseBeverage()
    {
        Console.WriteLine("Please insert money first");
    }

    public void CancelTransaction()
    {
        Console.WriteLine("No transaction to cancel");
    }
}