namespace ChainOfResponsibilityPattern.Naive.Models;

public class Product(int id, string name, decimal price, int stock)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));
    public decimal Price { get; set; } = price >= 0 ? price : throw new ArgumentException("Price cannot be negative", nameof(price));
    public int Stock { get; set; } = stock >= 0 ? stock : throw new ArgumentException("Stock cannot be negative", nameof(stock));
}