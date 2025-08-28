using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ChainOfResponsibilityPattern.CoR;
using ChainOfResponsibilityPattern.Naive;
using ChainOfResponsibilityPattern.Naive.Models;
using ChainOfResponsibilityPattern.Naive.Services;
using Settings;

namespace ChainOfResponsibilityPattern;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public void CoRApproach()
    {
        var orderService = new OrderProcessingService();
        
        var customer = new CoR.Models.Customer(1, "John Doe", 1000m);
        var product = new CoR.Models.Product(1, "Gaming Laptop", 899.99m, 5);
        
        Console.WriteLine("=== Successful Purchase ===");
        var result1 = orderService.ProcessOrder(customer, product);
        PrintResult(result1.Result.ToString(),
            result1.Message,
            result1.RemainingBalance,
            result1.RemainingStock,
            result1.IsSuccessful,
            result1.ProcessedAt);
        
        Console.WriteLine("\n=== Insufficient Funds ===");
        var expensiveProduct = new CoR.Models.Product(2, "Luxury Car", 50000m, 2);
        var result2 = orderService.ProcessOrder(customer, expensiveProduct);
        PrintResult(result2.Result.ToString(),
            result2.Message,
            result2.RemainingBalance,
            result2.RemainingStock,
            result2.IsSuccessful,
            result2.ProcessedAt);
        
        Console.WriteLine("\n=== Out of Stock ===");
        var result3 = orderService.ProcessOrder(customer, product, 10);
        PrintResult(result3.Result.ToString(),
            result3.Message,
            result3.RemainingBalance,
            result3.RemainingStock,
            result3.IsSuccessful,
            result3.ProcessedAt);
    }

    [Benchmark]
    public void NaiveApproach()
    {
        var inventoryService = new InventoryService();
        var paymentService = new PaymentService();
        var shippingService = new ShippingService();
        
        var orderService = new Order(inventoryService, paymentService, shippingService);
        
        var customer = new Customer(1, "John Doe", 1000m);
        var product = new Product(1, "Gaming Laptop", 899.99m, 5);
        
        Console.WriteLine("=== Successful Purchase ===");
        var result1 = orderService.Purchase(customer, product);
        
        PrintResult(result1.Result.ToString(),
            result1.Message,
            result1.RemainingBalance,
            result1.RemainingStock,
            result1.IsSuccessful,
            result1.ProcessedAt);
        
        Console.WriteLine("\n=== Insufficient Funds ===");
        var expensiveProduct = new Product(2, "Luxury Car", 50000m, 2);
        var result2 = orderService.Purchase(customer, expensiveProduct);
        
        PrintResult(result2.Result.ToString(),
            result2.Message,
            result2.RemainingBalance,
            result2.RemainingStock,
            result2.IsSuccessful,
            result2.ProcessedAt);
        
        Console.WriteLine("\n=== Out of Stock ===");
        var result3 = orderService.Purchase(customer, product, 10);
        PrintResult(result3.Result.ToString(),
            result3.Message,
            result3.RemainingBalance,
            result3.RemainingStock,
            result3.IsSuccessful,
            result3.ProcessedAt);
    }
    
    private static void PrintResult(string result,
        string? message,
        decimal remainingBalance,
        int remainingStock,
        bool success,
        DateTime processedAt)
    {
        Console.WriteLine($"Result: {result}");
        Console.WriteLine($"Message: {message}");
        Console.WriteLine($"Remaining Balance: {remainingBalance:C}");
        Console.WriteLine($"Remaining Stock: {remainingStock}");
        if (success)
        {
            Console.WriteLine($"Processed At: {processedAt}");
        }
    }
}