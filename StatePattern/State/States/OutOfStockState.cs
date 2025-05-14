namespace StatePattern.State.States;

public class OutOfStockState(CoffeeMachine coffeeMachine) : ICoffeeMachineState
{
    public void InsertMoney(decimal amount)
    {
        Console.WriteLine("Sorry, the machine is out of beverages. Returning your money.");
    }

    public void SelectBeverage()
    {
        Console.WriteLine("Sorry, the machine is out of beverages");
    }

    public void DispenseBeverage()
    {
        Console.WriteLine("Sorry, the machine is out of beverages");
    }

    public void CancelTransaction()
    {
        coffeeMachine.ReturnMoney();
    }
}