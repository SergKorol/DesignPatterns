using ChainOfResponsibilityPattern.Naive.Models;

namespace ChainOfResponsibilityPattern.Naive.Services;

public interface IPaymentService
{
    bool ProcessPayment(Customer customer, decimal amount);
    void RefundPayment(Customer customer, decimal amount);
}

public class PaymentService : IPaymentService
{
    public bool ProcessPayment(Customer customer, decimal amount)
    {
        if (customer.Balance < amount) return false;
        customer.Balance -= amount;
        return true;
    }

    public void RefundPayment(Customer customer, decimal amount)
    {
        customer.Balance += amount;
    }
}