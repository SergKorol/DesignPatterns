namespace StatePattern.State.States;

public class HasMoneyState(CoffeeMachine coffeeMachine) : ICoffeeMachineState
{
    private const decimal BeveragePrice = 1.50M;

    public void InsertMoney(decimal amount)
    {
        coffeeMachine.AddMoney(amount);
    }

    public void SelectBeverage()
    {
        if (coffeeMachine.BeverageCount <= 0)
        {
            Console.WriteLine("Sorry, out of beverages");
            return;
        }

        if (coffeeMachine.CurrentBalance >= BeveragePrice)
        {
            Console.WriteLine("Beverage selected!");
            coffeeMachine.SetState(new BeverageSelectedState(coffeeMachine));
        }
        else
        {
            Console.WriteLine(
                $"Insufficient funds. Please insert more money. Current balance: ${coffeeMachine.CurrentBalance}, Beverage price: ${BeveragePrice}");
        }
    }

    public void DispenseBeverage()
    {
        Console.WriteLine("Please select a beverage first");
    }

    public void CancelTransaction()
    {
        coffeeMachine.ReturnMoney();
        coffeeMachine.SetState(new NoMoneyState(coffeeMachine));
    }
}