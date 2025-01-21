namespace StrategyPattern.NonStrategy;

public interface IPayPalPayment
{
    void PayPalPay(decimal amount);
}