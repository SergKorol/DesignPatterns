namespace StatePattern.State.States;

public class BeverageSelectedState(CoffeeMachine coffeeMachine) : ICoffeeMachineState
{
    private const decimal BeveragePrice = 1.50M;

    public void InsertMoney(decimal amount)
    {
        Console.WriteLine("Beverage already selected. Please wait for beverage to be dispensed.");
    }

    public void SelectBeverage()
    {
        Console.WriteLine("Beverage already selected. Please wait for beverage to be dispensed.");
    }

    public void DispenseBeverage()
    {
        if (coffeeMachine.BeverageCount > 0)
        {
            coffeeMachine.DecrementBeverageCount();
            Console.WriteLine("Beverage dispensed!");

            decimal changeAmount = coffeeMachine.CurrentBalance - BeveragePrice;
            if (changeAmount > 0)
            {
                Console.WriteLine($"Returning change: ${changeAmount}");
            }

            coffeeMachine.SetState(new NoMoneyState(coffeeMachine));
            coffeeMachine.CurrentBalance = 0;
        }
        else
        {
            Console.WriteLine("Sorry, out of beverages");
            coffeeMachine.ReturnMoney();
            coffeeMachine.SetState(new NoMoneyState(coffeeMachine));
        }
    }

    public void CancelTransaction()
    {
        coffeeMachine.ReturnMoney();
        coffeeMachine.SetState(new NoMoneyState(coffeeMachine));
    }
}