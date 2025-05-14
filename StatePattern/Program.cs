using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Settings;

namespace StatePattern;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public void RunState()
    {
        State.CoffeeMachine coffeeMachine = new State.CoffeeMachine(5);

        // Try operations in NoMoneyState
        Console.WriteLine("\n[NoMoneyState]");
        Console.WriteLine($"\nCurrent State: {coffeeMachine.GetCurrentState()}");
        coffeeMachine.SelectBeverage(); // Should fail

        // Insert money and move to HasMoneyState
        Console.WriteLine("\n[Moving to HasMoneyState]");
        coffeeMachine.InsertMoney(1.00M);
        Console.WriteLine($"\nCurrent State: {coffeeMachine.GetCurrentState()}");

        // Insufficient funds
        coffeeMachine.SelectBeverage();

        // Add more money
        coffeeMachine.InsertMoney(1.00M);

        // Now select beverage to move to BeverageSelectedState
        Console.WriteLine("\n[Moving to BeverageSelectedState]");
        coffeeMachine.SelectBeverage();
        Console.WriteLine($"Current State: {coffeeMachine.GetCurrentState()}");

        // Try to select again or insert money
        coffeeMachine.SelectBeverage();
        coffeeMachine.InsertMoney(0.50M);

        // Dispense beverage and go back to NoMoneyState
        Console.WriteLine("\n[Dispensing Beverage]");
        coffeeMachine.DispenseBeverage();
        Console.WriteLine($"Current State: {coffeeMachine.GetCurrentState()}");

        // We're back in NoMoneyState, try operations
        Console.WriteLine("\n[Back to NoMoneyState]");
        coffeeMachine.SelectBeverage();

        // Try cancel transaction
        Console.WriteLine("\n[Cancel transaction from different states]");
        coffeeMachine.CancelTransaction();
        coffeeMachine.InsertMoney(2.00M);
        Console.WriteLine($"Current State: {coffeeMachine.GetCurrentState()}");
        coffeeMachine.CancelTransaction();
        Console.WriteLine($"Current State: {coffeeMachine.GetCurrentState()}");

        Console.WriteLine("\n[Out Of Stock]");
        State.CoffeeMachine emptyMachine = new State.CoffeeMachine(0);
        Console.WriteLine($"Current State: {emptyMachine.GetCurrentState()}");
        emptyMachine.InsertMoney(1.00M);
    }

    [Benchmark]
    public void RunNaive()
    {
        Naive.CoffeeMachine coffeeMachine = new Naive.CoffeeMachine(5);

        // Try operations in NoMoney state
        Console.WriteLine("\n[NoMoneyState]");
        Console.WriteLine($"\nCurrent State: {coffeeMachine.GetCurrentState()}");
        coffeeMachine.SelectBeverage();

        // Insert money and move to HasMoney state
        Console.WriteLine("\n[Moving to HasMoneyState]");
        coffeeMachine.InsertMoney(1.00M);
        Console.WriteLine($"Current State: {coffeeMachine.GetCurrentState()}");

        // Insufficient funds
        coffeeMachine.SelectBeverage();

        // Add more money
        coffeeMachine.InsertMoney(1.00M);

        // Now select beverage to move to BeverageSelected state
        Console.WriteLine("\n[Moving to BeverageSelectedState]");
        coffeeMachine.SelectBeverage();
        Console.WriteLine($"Current State: {coffeeMachine.GetCurrentState()}");

        // Try to select again or insert money
        coffeeMachine.SelectBeverage();
        coffeeMachine.InsertMoney(0.50M);

        // Dispense beverage and go back to NoMoney state
        Console.WriteLine("\n[Dispensing Beverage]");
        coffeeMachine.DispenseBeverage();
        Console.WriteLine($"Current State: {coffeeMachine.GetCurrentState()}");

        // We're back in NoMoney state, try operations
        Console.WriteLine("\n[Back to NoMoneyState]");
        coffeeMachine.SelectBeverage();

        // Try cancel transaction
        Console.WriteLine("\n[Cancel transaction from different states]");
        coffeeMachine.CancelTransaction();
        coffeeMachine.InsertMoney(2.00M);
        Console.WriteLine($"Current State: {coffeeMachine.GetCurrentState()}");
        coffeeMachine.CancelTransaction();
        Console.WriteLine($"Current State: {coffeeMachine.GetCurrentState()}");

        // Create a vending machine with 0 beverages to demonstrate OutOfStock state
        Console.WriteLine("\n[Out Of Stock]");
        Naive.CoffeeMachine emptyMachine = new Naive.CoffeeMachine(0);
        Console.WriteLine($"Current State: {emptyMachine.GetCurrentState()}");
        emptyMachine.InsertMoney(1.00M);
    }
}