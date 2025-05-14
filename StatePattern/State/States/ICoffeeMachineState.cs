namespace StatePattern.State.States;

public interface ICoffeeMachineState
{
    void InsertMoney(decimal amount);
    void SelectBeverage();
    void DispenseBeverage();
    void CancelTransaction();
}