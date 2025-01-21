namespace StrategyPattern.Strategy;

public class PaymentContext : IPaymentContext
{
    private IPayment? _paymentStrategy;

    public void SetPaymentStrategy(IPayment? payment)
    {
        _paymentStrategy = payment;
    }

    public void ExecutePayment(decimal amount)
    {
        if (_paymentStrategy == null)
        {
            throw new InvalidOperationException("Payment strategy is not set.");
        }

        _paymentStrategy.Pay(amount);
    }
}
