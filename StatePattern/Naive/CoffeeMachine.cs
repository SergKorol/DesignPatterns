namespace StatePattern.Naive;

public class CoffeeMachine(int beverageCount)
{
    private CoffeeMachineState _currentState =
        beverageCount <= 0 ? CoffeeMachineState.OutOfStockState : CoffeeMachineState.NoMoneyState;

    private int BeverageCount { get; set; } = beverageCount;
    private decimal CurrentBalance { get; set; }
    private const decimal BeveragePrice = 1.50M;

    public void InsertMoney(decimal amount)
    {
        switch (_currentState)
        {
            case CoffeeMachineState.NoMoneyState:
                if (amount > 0)
                {
                    AddMoney(amount);
                    _currentState = CoffeeMachineState.HasMoneyState;
                }
                else
                {
                    Console.WriteLine("Please insert a valid amount of money");
                }

                break;

            case CoffeeMachineState.HasMoneyState:
                AddMoney(amount);
                break;

            case CoffeeMachineState.BeverageSelectedState:
                Console.WriteLine("Beverage already selected. Please wait for beverage to be dispensed.");
                break;

            case CoffeeMachineState.OutOfStockState:
                Console.WriteLine("Sorry, the machine is out of beverages. Returning your money.");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SelectBeverage()
    {
        switch (_currentState)
        {
            case CoffeeMachineState.NoMoneyState:
                Console.WriteLine("Please insert money first");
                break;

            case CoffeeMachineState.HasMoneyState:
                if (BeverageCount <= 0)
                {
                    Console.WriteLine("Sorry, out of beverages");
                    _currentState = CoffeeMachineState.OutOfStockState;
                    return;
                }

                if (CurrentBalance >= BeveragePrice)
                {
                    Console.WriteLine("Beverage selected!");
                    _currentState = CoffeeMachineState.BeverageSelectedState;
                }
                else
                {
                    Console.WriteLine(
                        $"Insufficient funds. Please insert more money. Current balance: ${CurrentBalance}, Beverage price: ${BeveragePrice}");
                }

                break;

            case CoffeeMachineState.BeverageSelectedState:
                Console.WriteLine("Beverage already selected. Please wait for beverage to be dispensed.");
                break;

            case CoffeeMachineState.OutOfStockState:
                Console.WriteLine("Sorry, the machine is out of beverages");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void DispenseBeverage()
    {
        switch (_currentState)
        {
            case CoffeeMachineState.NoMoneyState:
                Console.WriteLine("Please insert money first");
                break;

            case CoffeeMachineState.HasMoneyState:
                Console.WriteLine("Please select a beverage first");
                break;

            case CoffeeMachineState.BeverageSelectedState:
                if (BeverageCount > 0)
                {
                    BeverageCount--;
                    Console.WriteLine("Beverage dispensed!");

                    decimal changeAmount = CurrentBalance - BeveragePrice;
                    if (changeAmount > 0)
                    {
                        Console.WriteLine($"Returning change: ${changeAmount}");
                    }

                    CurrentBalance = 0;

                    _currentState = BeverageCount <= 0
                        ? CoffeeMachineState.OutOfStockState
                        : CoffeeMachineState.NoMoneyState;
                }
                else
                {
                    Console.WriteLine("Sorry, out of beverages");
                    ReturnMoney();
                    _currentState = CoffeeMachineState.OutOfStockState;
                }

                break;

            case CoffeeMachineState.OutOfStockState:
                Console.WriteLine("Sorry, the machine is out of beverages");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void CancelTransaction()
    {
        switch (_currentState)
        {
            case CoffeeMachineState.NoMoneyState:
                Console.WriteLine("No transaction to cancel");
                break;

            case CoffeeMachineState.HasMoneyState:
            case CoffeeMachineState.BeverageSelectedState:
                ReturnMoney();
                _currentState = CoffeeMachineState.NoMoneyState;
                break;

            case CoffeeMachineState.OutOfStockState:
                ReturnMoney();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void AddMoney(decimal amount)
    {
        CurrentBalance += amount;
        Console.WriteLine($"Money added: ${amount}. Current balance: ${CurrentBalance}");
    }

    private void ReturnMoney()
    {
        if (CurrentBalance <= 0) return;
        Console.WriteLine($"Returning ${CurrentBalance} to customer");
        CurrentBalance = 0;
    }

    public string GetCurrentState()
    {
        return _currentState.ToString();
    }
}