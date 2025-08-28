namespace ChainOfResponsibilityPattern.CoR.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }

    public Customer(int id, string name, decimal balance)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Balance = balance >= 0 ? balance : throw new ArgumentException("Balance cannot be negative", nameof(balance));
    }
}