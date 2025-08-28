namespace ChainOfResponsibilityPattern.Naive.Models;

public class Customer(int id, string name, decimal balance)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));
    public decimal Balance { get; set; } = balance >= 0 ? balance : throw new ArgumentException("Balance cannot be negative", nameof(balance));
}