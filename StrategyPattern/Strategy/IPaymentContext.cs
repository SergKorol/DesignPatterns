namespace StrategyPattern.Strategy;

public interface IPaymentContext
{
    void SetPaymentStrategy(IPayment? payment);

    void ExecutePayment(decimal amount);
}